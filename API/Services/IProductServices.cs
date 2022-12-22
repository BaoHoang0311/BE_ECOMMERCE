using API.Dtos;
using API.Entites;
using API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IProductServices : IEntityBaseRepository<Product>
    {
    }
}
