using Common.Utils.Tools.Models;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading;

namespace ETicketing.CA.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IServiceScope _scope;
        public string ContextUserId { get; set; }
        public DbContext DbContext { get; set; }
        private IDbContextTransaction _transaction;
        List<AuditEntityDTO> Changes = new List<AuditEntityDTO>();
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _scope = serviceProvider.CreateScope();
            DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }
        public void Configure(IServiceScope scope, string userId, bool useTransaction)
        {
            ContextUserId = userId;
            _scope = scope;
            DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (useTransaction)
            {
                _transaction = DbContext.Database.BeginTransaction();
            }
        }


        public T GetService<T>() where T : notnull
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }


        async public Task SaveChangesAsync()
        {
            await SaveChangesBaseAsync(() => DbContext.SaveChangesAsync());
        }
        public void SaveChanges()
        {
            SaveChangesBaseAsync(() => Task.Run(() => DbContext.SaveChanges()))
                .GetAwaiter()
                .GetResult();
        }




        public List<AuditEntityDTO> Commit()
        {
            _transaction?.Commit();
            return Changes;
        }
        public void Rollback() => _transaction?.Rollback();

        public void Dispose()
        {
            _transaction?.Dispose();
            DbContext.Dispose();
            _scope.Dispose();
        }


        async public Task SaveChangesBaseAsync(Func<Task> saveChangesFunc)
        {
            DateTime now = DateTime.UtcNow;
            var changes = DbContext.ChangeTracker.Entries();
            List<(AuditEntityDTO, List<PropertyEntry>)> audits = CalculateChangesBeforeSave(now, changes);
            await saveChangesFunc();
            Changes.AddRange(FinishAllChanges(now, audits));
        }

        public List<(AuditEntityDTO Entity, List<PropertyEntry> ToChangeAfterSave)> CalculateChangesBeforeSave(DateTime now, IEnumerable<EntityEntry> changes)
        {
            //Column Audit
            //ADD
            foreach (EntityEntry entry in changes.Where(x => x.State == EntityState.Added && x.Entity is IHasCreateAudit))
            {
                MarkAuditAs(EntityState.Added, entry, now);
            }
            //MODIFIED
            foreach (EntityEntry entry in changes.Where(x => x.State == EntityState.Modified && (x.Entity is IHasUpdateAudit || x.Entity is IHasDeleteAudit)))
            {

                //UPDATED
                if (entry.Entity is IHasUpdateAudit && (entry.Entity is not IHasDeleteAudit || entry.Properties.Any(x => x.IsModified && x.Metadata.Name != nameof(IHasDeleteAudit.IsDeleted))))
                {
                    MarkAuditAs(EntityState.Modified, entry, now);
                }
                //LOGIC DELETE
                if (entry.Entity is IHasDeleteAudit && entry.Property(nameof(IHasDeleteAudit.IsDeleted)).IsModified && ((bool)entry.Property(nameof(IHasDeleteAudit.IsDeleted)).CurrentValue) == true)
                {
                    MarkAuditAs(EntityState.Deleted, entry, now);
                }
                if (entry.Entity is IHasCreateAudit)
                {
                    entry.Property(nameof(IHasCreateAudit.CreationTime)).IsModified = false;
                    entry.Property(nameof(IHasCreateAudit.CreatorId)).IsModified = false;
                }
            }

            //TableAudit
            List<(AuditEntityDTO Entity, List<PropertyEntry> ToChangeAfterSave)> entityChanges = new List<(AuditEntityDTO, List<PropertyEntry>)>();
            foreach (EntityEntry entry in changes.Where(x => new List<EntityState> { EntityState.Added, EntityState.Modified, EntityState.Deleted }.Contains(x.State) && x.Entity is IFullAudit))
            {
                AuditEntityDTO entity = new AuditEntityDTO
                {
                    EntityName = entry.Entity.GetType().Name,
                };
                List<PropertyEntry> changed = new List<PropertyEntry>();
                FindEntityId(entity, entry.Properties.ToList());
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.EntityChangeType = EntityChangeType.Added;
                        changed = entry.Properties.Where(x => x.IsTemporary).ToList();
                        CalculateEntityChanges(entity, entry.Properties.Where(x => !x.IsTemporary).ToList());
                        break;
                    case EntityState.Modified:
                        entity.EntityChangeType = EntityChangeType.Updated;
                        changed = entry.Properties.Where(x => x.IsTemporary).ToList();
                        CalculateEntityChanges(entity, entry.Properties.Where(x => x.IsModified && !x.IsTemporary).ToList());
                        break;
                    case EntityState.Deleted:
                        entity.EntityChangeType = EntityChangeType.Deleted;
                        CalculateEntityChanges(entity, entry.Properties.Where(x => !x.IsTemporary).ToList());
                        break;
                }
                entityChanges.Add((entity, changed));
            }
            return entityChanges;
        }

        private void MarkAuditAs(EntityState added, EntityEntry entry, DateTime now)
        {
            switch (added)
            {
                case EntityState.Added:
                    var entityA = entry.Entity as IHasCreateAudit;
                    entityA.CreationTime = now;
                    entityA.CreatorId = ContextUserId;
                    entry.Property(nameof(IHasCreateAudit.CreationTime)).IsModified = true;
                    entry.Property(nameof(IHasCreateAudit.CreatorId)).IsModified = true;
                    break;
                case EntityState.Modified:
                    var entityM = entry.Entity as IHasUpdateAudit;
                    entityM.LastModificationTime = now;
                    entityM.LastModifierId = ContextUserId;
                    entry.Property(nameof(IHasUpdateAudit.LastModificationTime)).IsModified = true;
                    entry.Property(nameof(IHasUpdateAudit.LastModifierId)).IsModified = true;
                    break;
                case EntityState.Deleted:
                    var entityD = entry.Entity as IHasDeleteAudit;
                    entityD.DeletionTime = now;
                    entityD.DeleterId = ContextUserId;
                    entry.Property(nameof(IHasDeleteAudit.DeletionTime)).IsModified = true;
                    entry.Property(nameof(IHasDeleteAudit.DeleterId)).IsModified = true;
                    break;
                default:
                    break;
            }
        }

        public List<AuditEntityDTO> FinishAllChanges(DateTime now, List<(AuditEntityDTO, List<PropertyEntry>)> audits)
        {
            foreach ((AuditEntityDTO, List<PropertyEntry>) item in audits)
            {
                CalculateEntityChanges(item.Item1, item.Item2);
            }
            return audits.Select(x => x.Item1).ToList();
        }
        private void FindEntityId(AuditEntityDTO entityChanges, List<PropertyEntry> changes)
        {
            if (entityChanges.EntityId == null && changes.Any(x => x.IsTemporary == false && x.Metadata.IsPrimaryKey()))
            {
                var pkValues = changes
                    .Where(x => x.Metadata.IsPrimaryKey() && x.IsTemporary == false)
                    .Select(x => x.CurrentValue?.ToString())
                    .ToList();

                if (pkValues.Count > 0)
                {                    
                    entityChanges.EntityId = string.Join("|", pkValues);
                }
            }
        }
        private void CalculateEntityChanges(AuditEntityDTO entityChanges, List<PropertyEntry> changes)
        {
            FindEntityId(entityChanges, changes);

            foreach (PropertyEntry pe in changes)
            {
                AuditChangeDTO ec = new AuditChangeDTO();
                ec.FieldName = pe.Metadata.Name;
                if (new List<EntityChangeType> { EntityChangeType.Added, EntityChangeType.Updated }.Contains(entityChanges.EntityChangeType) && pe.CurrentValue != null)
                {
                    ec.NewValue = FormatDatum(pe.CurrentValue);
                }
                if (new List<EntityChangeType> { EntityChangeType.Deleted, EntityChangeType.Updated }.Contains(entityChanges.EntityChangeType) && pe.OriginalValue != null)
                {
                    ec.OldValue = FormatDatum(pe.OriginalValue);
                }
                entityChanges.Changes.Add(ec);
            }
        }
        private string FormatDatum(object datum)
        {
            string result = null;
            if (datum != null)
            {
                if (datum.GetType() == typeof(DateTime))
                {
                    result = ((DateTime)DateTime.SpecifyKind(((DateTime)datum), DateTimeKind.Unspecified)).ToString("o", CultureInfo.InvariantCulture);
                }
                else
                {
                    result = datum.ToString();
                }
            }
            return result;
        }
    }
}
