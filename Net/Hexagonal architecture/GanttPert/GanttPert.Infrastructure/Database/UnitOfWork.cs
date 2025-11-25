using GanttPert.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IServiceScope _scope;
        public DbContext DbContext { get; set; }
        private IDbContextTransaction _transaction;
        public void Configure(IServiceScope scope, bool useTransaction)
        {
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


        
        public void SaveChanges() => DbContext.SaveChanges();

        public void Commit() => _transaction?.Commit();
        public void Rollback() => _transaction?.Rollback();

        public void Dispose()
        {
            _transaction?.Dispose();
            DbContext.Dispose();
            _scope.Dispose();
        }
    }
}
