using API.Data;
using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductServices : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;
        public ProductServices(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllAsyncSearchandPaging(IEnumerable<Product> source, string searchString, int? pageNumber, int pageSize)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                source = source.Where(x => x.FullName.Contains(searchString.Trim().ToLower())).ToList();
            }
            source = Pagging<Product>.Create(source.AsQueryable(), pageNumber ?? 1, pageSize);
            return source;
        }
    }
}
