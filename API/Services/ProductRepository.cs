﻿using API.Data;
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
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {

        public ProductRepository(MyDbContext context) : base(context)
        {
        }

    }
}