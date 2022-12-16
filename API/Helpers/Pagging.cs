using API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Helpers
{
    public class Pagging<T> : List<T> 
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public Pagging(List<T> items, int count, int pageIndex , int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public static Pagging<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new Pagging<T>(items, count, pageIndex, pageSize);
        }
    }
}
