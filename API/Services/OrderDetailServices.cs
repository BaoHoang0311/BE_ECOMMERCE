using API.Controllers;
using API.Data;
using API.Entites;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class OrderDetailServices : EntityBaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly MyDbContext _context;
        public OrderDetailServices(MyDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDetail>> GetAllListById(int id)
        {
            var data = await _context.OrderDetails.Where(x => x.OrderId == id).ToListAsync();
            return data;
        }
    }
}
