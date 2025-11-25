using GanttPert.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Abstractions.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        void Configure(IServiceScope scope, bool useTransaction);
        T GetService<T>();
        void SaveChanges();   
        void Commit();        
        void Rollback();      
    }
}
