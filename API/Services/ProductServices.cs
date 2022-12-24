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
    public class ProductServices : EntityBaseRepository<Product>, IProductServices
    {

        public ProductServices(MyDbContext context) : base(context)
        {
        }

    }
}
// main : 12h33
