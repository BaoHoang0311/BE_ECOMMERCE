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
    public class OrderDetailRepository : EntityBaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly MyDbContext _context;
        public OrderDetailRepository(MyDbContext context) : base(context)
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
