using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Repository
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> Spec)
        {
            var Query = inputQuery;
            if(Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }
            Query = Spec.Includes.Aggregate(Query,(CurrentQuery,IncludeExpression)=>CurrentQuery.Include(IncludeExpression));
            return Query;

        }
         
    }
}
