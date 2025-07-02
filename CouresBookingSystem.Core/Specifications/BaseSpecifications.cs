using CouresBookingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>> ();

        //GetAll 
        public BaseSpecifications()
        {

        }
        //ById
        public BaseSpecifications(Expression<Func<T, bool>> ex_Criteria)
        {
            Criteria = ex_Criteria;
        }

    }
}
