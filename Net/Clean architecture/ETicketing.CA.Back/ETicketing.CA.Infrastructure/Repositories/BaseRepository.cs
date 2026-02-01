
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TId>: IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : notnull
    {
        public IUnitOfWork _unitOfWork;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        virtual public async Task<List<TEntity>> GetAllAsync()
        {
            return await _unitOfWork.DbContext.Set<TEntity>()
                .ToListAsync();
        }
        virtual public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _unitOfWork.DbContext.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id!.Equals(id));
        }
        public async virtual Task AddAsync(TEntity entity)
        {
           await _unitOfWork.DbContext.AddAsync(entity);
        }
        virtual public async Task DeleteByIdAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.DbContext.Remove(entity);
            }
        }
    }
}
