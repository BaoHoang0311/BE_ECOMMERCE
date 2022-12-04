using Core.Entites;
using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        Task AddAsync(ProductVM entity);

        Task UpdateAsync(string id, ProductVM entity);
    }
}
