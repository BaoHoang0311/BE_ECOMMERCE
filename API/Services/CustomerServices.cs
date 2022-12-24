using API.Data;
using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class CustomerServices : EntityBaseRepository<Customer>, ICustomerServices
    {
        public CustomerServices(MyDbContext context) : base(context)
        {

        }
    }
}

