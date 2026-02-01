
using ETicketing.CA.Domain.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Domain.Abstractions.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        string ContextUserId { get; }

        void Configure(IServiceScope scope, string userId, bool useTransaction);
        T GetService<T>();
        void SaveChanges();
        List<AuditEntityDTO> Commit();        
        void Rollback();      
    }
}
