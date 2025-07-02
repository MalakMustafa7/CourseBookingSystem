using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Repositories;
using CouresBookingSystem.Core.Specifications;
using CouresBookingSystem.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly BookingContext _dbContect;

        public GenericRepository(BookingContext dbContect)
        {
            _dbContect = dbContect;
        }
        public async Task AddAsync(T entity)
        {
           await  _dbContect.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        => _dbContect.Set<T>().Remove(entity);
        

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
             return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContect.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdGetEntityWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        public void Update(T entity)
         => _dbContect.Set<T>().Update(entity);

        private IQueryable<T> ApplySpecification(ISpecification<T> Spec) { 
            return  SpecificationEvalutor<T>.GetQuery(_dbContect.Set<T>(), Spec);
        }
    }
}
