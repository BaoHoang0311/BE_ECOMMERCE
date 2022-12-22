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
    public class BuyOrderDetailRepository : EntityBaseRepository<BuyOrderDetail>, IBuyOrderDetailRepository
    {
        private readonly MyDbContext _context;
        public BuyOrderDetailRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BuyOrderDetail>> GetAllListById(int id)
        {
            var data = await _context.BuyOrderDetails.Where(x => x.BuyOrderId == id).ToListAsync();
            return data;
        }

        Task<IEnumerable<BuyOrderDetail>> IBuyOrderDetailRepository.GetAllListById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
