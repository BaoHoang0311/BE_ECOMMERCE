using API.Data;
using API.Dtos;
using API.Entites;
using API.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Services
{
    public class BuyOrderServices : EntityBaseRepository<BuyOrder>,IBuyOrderRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public BuyOrderServices(MyDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<BuyOrder>> GetBuyOrderbyOrderId(int BuyOrderId )
        {
            var listorder = await _context.BuyOrders.Include(o => o.BuyOrderDetails).ThenInclude(o => o.Product)
                                                .ToListAsync();

            if (listorder != null)
            {
                listorder = listorder.Where(x => x.Id == BuyOrderId).ToList();
                return listorder;
            }

            return null;
        }

        public async Task<bool> AddBuyOrderAsync(BuyOrderDtos orderDtos)
        {
            throw new NotImplementedException();
        }
        //private bool UpdateAmmount(BuyOrderDetailDtos buyorderDetailDtos)
        //{
        //    var sp = _context.Products.FirstOrDefault(x => x.Id == buyorderDetailDtos.ProductId);

        //    if (sp != null)
        //    {
        //        sp.Amount = sp.Amount - orderDetailDtos.ProductAmmount;

        //        _context.Products.Attach(sp);
        //        _context.Entry(sp).Property(x => x.Amount).IsModified = true;
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}


        public async  Task<bool> UpdateBuyOrder(BuyOrderDtos orderDtos)
        {
            throw new NotImplementedException();
        }
    }
}
