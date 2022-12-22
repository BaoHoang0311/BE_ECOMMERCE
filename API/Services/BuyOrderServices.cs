using API.Data;
using API.Dtos;
using API.Entites;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using API.Repository;

namespace API.Services
{
    public class BuyOrderServices : EntityBaseRepository<BuyOrder>, IBuyOrderServices
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public BuyOrderServices(MyDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<BuyOrder>> GetBuyOrderbyOrderId(int BuyOrderId)
        {
            var listorder = await _context.BuyOrders.Where(x => x.Id == BuyOrderId).Include(o => o.BuyOrderDetails).ThenInclude(o => o.Product)
                                                .ToListAsync();

            if (listorder != null)
            {
                listorder = listorder.Where(x => x.Id == BuyOrderId).ToList();
                return listorder;
            }

            return null;
        }

        public async Task<bool> AddBuyOrderAsync(BuyOrderDtos buyorderDtos)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(o => o.Id == buyorderDtos.CustomerId);
            if (cus != null)
            {
                var buyorder = _mapper.Map<BuyOrder>(buyorderDtos);

                buyorder.CreatedBy = "admin";
                buyorder.CreatedDate = DateTime.Now;
                buyorder.ModifiedDate = DateTime.Now;
                buyorder.ModifiedBy = "admin";

                buyorder.TotalPrice = TotalPrice(buyorder.BuyOrderDetails);

                if (buyorder.TotalPrice > 0)
                {
                    var list = buyorder.BuyOrderDetails;
                    buyorder.BuyOrderDetails = null;

                    await _context.BuyOrders.AddAsync(buyorder);
                    await _context.SaveChangesAsync();


                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (UpdateAmmount(item))
                            {
                                var buyorderDetail = _mapper.Map<BuyOrderDetail>(item);

                                buyorderDetail.OrderNo = "";

                                buyorderDetail.CreatedBy = "";
                                buyorderDetail.CreatedDate = DateTime.Now;
                                buyorderDetail.ModifiedDate = DateTime.Now;
                                buyorderDetail.BuyOrderId = buyorder.Id;

                                buyorderDetail.TotalPrice = item.price * item.ammount;

                                await _context.BuyOrderDetails.AddAsync(buyorderDetail);

                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                return false;
            }
            return false;
        }
        private decimal TotalPrice(List<BuyOrderDetail> buyorderDetail)
        {
            decimal res = 0;
            if (buyorderDetail.Count > 0)
            {
                foreach (var item in buyorderDetail)
                {
                    res += item.ammount * item.price;
                }
            }
            return res;
        }

        private bool UpdateAmmount(BuyOrderDetail buyorderDetail)
        {
            var sp = _context.Products.FirstOrDefault(x => x.Id == buyorderDetail.ProductId);

            if (sp != null)
            {
                sp.Amount = sp.Amount + buyorderDetail.ammount;

                _context.Products.Attach(sp);
                _context.Entry(sp).Property(x => x.Amount).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateBuyOrder(BuyOrderDtos buyorderDtos)
        {
            var data = await _context.BuyOrders.AsNoTracking().Include(m => m.BuyOrderDetails).FirstOrDefaultAsync(o => o.Id == buyorderDtos.BuyOrderId);
            var buyorder = _mapper.Map<BuyOrder>(buyorderDtos);

            //order.TotalPrice = TotalPrice(orderDtos.orderDetailDtos);
            buyorder.ModifiedDate = DateTime.Now;
            buyorder.CreatedDate = data.CreatedDate;

            foreach (var item in buyorder.BuyOrderDetails)
            {
                item.TotalPrice = item.price * item.ammount;
                item.ModifiedDate = DateTime.Now;
                item.CreatedDate = buyorder.CreatedDate;
            }

            _context.Update(buyorder);
            await _context.SaveChangesAsync();
            return true;
        }
        // FE xử lý
        public async Task<bool> AddBuyOrderAsync_1(BuyOrderDtos buyorderDtos)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(o => o.Id == buyorderDtos.CustomerId);
            if (cus != null)
            {
                var buyorder = _mapper.Map<BuyOrder>(buyorderDtos);

                buyorder.CreatedBy = "admin";
                buyorder.CreatedDate = DateTime.Now;
                buyorder.ModifiedDate = DateTime.Now;
                buyorder.ModifiedBy = "admin";

                if (buyorder.TotalPrice > 0)
                {
                    var list = buyorder.BuyOrderDetails;
                    buyorder.BuyOrderDetails = null;

                    await _context.BuyOrders.AddAsync(buyorder);
                    await _context.SaveChangesAsync();


                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (UpdateAmmount(item))
                            {
                                var buyorderDetail = _mapper.Map<BuyOrderDetail>(item);

                                buyorderDetail.OrderNo = "";

                                buyorderDetail.CreatedBy = "";
                                buyorderDetail.CreatedDate = DateTime.Now;
                                buyorderDetail.ModifiedDate = DateTime.Now;
                                buyorderDetail.BuyOrderId = buyorder.Id;

                                await _context.BuyOrderDetails.AddAsync(buyorderDetail);

                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
