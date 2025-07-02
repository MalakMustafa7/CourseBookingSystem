using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Specifications;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity);
        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        public Task<T> GetByIdGetEntityWithSpecAsync(ISpecification<T> Spec);
        public Task<T> GetByIdAsync(int id);
        public void Update(T entity);
        public void Delete(T entity);

    }
}
