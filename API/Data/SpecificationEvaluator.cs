
using API.Repository;
using API.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data
{
    public static class SpecificationEvaluator<T> where T : class, IEntityID, new()
    {
        public static IQueryable<T> GetQuery(IQueryable<T> ipquery, ISpecification<T> spec)
        {
            var query = ipquery;
            if(spec.Condition != null)
            {
                query = query.Where(spec.Condition);
            }
            // spec.Includes ~ array
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
