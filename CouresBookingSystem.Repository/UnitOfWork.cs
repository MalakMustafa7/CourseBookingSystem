using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Repositories;
using CouresBookingSystem.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _dbContext;
        private readonly Hashtable _repo;

        public UnitOfWork(BookingContext dbContext)
        {
            _dbContext = dbContext;
            _repo = new Hashtable();
        }
        public async Task<int> CompleteAsync()
         => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
         => await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repo.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(_dbContext);
                _repo.Add(type, Repository);
            }

             return _repo[type]as IGenericRepository<TEntity>;
        }
    }
}
