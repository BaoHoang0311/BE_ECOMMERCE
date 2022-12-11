﻿using API.Dtos;
using API.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        IEnumerable<Product> GetAllAsyncSearchandPaging(IEnumerable<Product> source, string searchString ,int? pageNumber, int pageSize);
    }
}