using API.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PaggingInfo<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public List<T> results { get; set; }
    }
    public class Pagging<T> 
    {
        public async static Task<PaggingInfo<T>> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            int PageIndex = pageIndex;

            int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            var res = new PaggingInfo<T>()
            {
                PageIndex = PageIndex,
                TotalPages = TotalPages,
                results = items,
            };
            return res ;
        }
    }

}
