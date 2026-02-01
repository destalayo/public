
using ETicketing.CA.Domain.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Domain.Abstractions.Interfaces
{
    public interface IRepository<TEntity,TId> 
        where TEntity : Entity<TId>
        where TId : notnull
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task DeleteByIdAsync(TId id);
    }
}
