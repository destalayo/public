using ETicketing.CA.Domain.Abstractions.Models;

namespace ETicketing.CA.Application.Abstractions
{
    public interface IAuditScope
    {
        AuditDTO Audit { get; }

        void AddChanges(List<AuditEntityDTO> changes);
        void AddChild(AuditDTO child);
        Task<T> RegisterAudit<T>(string userId, IAuditScope? parent, Func<Task<T>> p);
        Task RegisterAudit(string userId, IAuditScope? parent, Func<Task> p);
    }
}
