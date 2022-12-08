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
    public class OrderServices : EntityBaseRepository<Order>, IOrderRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public OrderServices(MyDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddOrderAsync(OrderDtos orderDtos)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(o => o.Id == orderDtos.CustomerId);
            if (cus != null)
            {
                var order = _mapper.Map<Order>(orderDtos);

                order.CreatedBy = "admin";
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;
                order.ModifiedBy = "admin";

                order.TotalPrice = TotalPrice(orderDtos.orderDetailDtos);

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();



                if (orderDtos.orderDetailDtos.Count > 0)
                {
                    foreach (var item in orderDtos.orderDetailDtos)
                    {
                        if (checkammount(item) == true && UpdateAmmount(item))
                        {
                            var orderDetail = _mapper.Map<OrderDetail>(item);

                            orderDetail.OrderNo = "";

                            orderDetail.CreatedBy = "";
                            orderDetail.CreatedDate = DateTime.Now;
                            orderDetail.ModifiedDate = DateTime.Now;
                            orderDetail.OrderId = order.Id;

                            orderDetail.TotalPrice = item.Price * item.ProductAmmount;

                            _context.OrderDetails.Add(orderDetail);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private decimal TotalPrice(List<OrderDetailDtos> orderDetailDtos)
        { 
            decimal res = 0;
            if (orderDetailDtos.Count > 0)
            {
                foreach (var item in orderDetailDtos)
                {
                    if (checkammount(item) == true)
                    {
                        res += item.ProductAmmount * item.Price;
                    }
                }
            }
            return res;
        }

        public async Task<IList<Order>> GetOrderbyOrderId(int OrderId)
        {
            var listorder = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product)
                                                .ToListAsync();

            if (listorder != null)
            {
                listorder = listorder.Where(x => x.Id == OrderId).ToList();
                return listorder;
            }

            return null;
        }

        public async Task<bool> UpdateOrder(OrderDtos orderDtos)
        {
            var data = await _context.Orders.AsNoTracking().Include(m => m.OrderDetails).FirstOrDefaultAsync(o => o.Id == orderDtos.OrderId);
            var order = _mapper.Map<Order>(orderDtos);

            //order.TotalPrice = TotalPrice(orderDtos.orderDetailDtos);
            order.ModifiedDate = DateTime.Now;
            order.CreatedDate = data.CreatedDate;

            foreach (var item in order.OrderDetails)
            {
                item.TotalPrice = item.price * item.ammount;
                item.ModifiedDate = DateTime.Now;
                item.CreatedDate = order.CreatedDate;
            }

            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }


        private bool checkammount(OrderDetailDtos orderDetail)
        {
            var sp = _context.Products.FirstOrDefault(x => x.Id == orderDetail.ProductId);

            if (sp != null && sp.Amount >= orderDetail.ProductAmmount)
            {
                return true;
            }
            return false;
        }

        private bool UpdateAmmount(OrderDetailDtos orderDetailDtos)
        {
            var sp = _context.Products.FirstOrDefault(x => x.Id == orderDetailDtos.ProductId);

            if (sp != null)
            {
                sp.Amount = sp.Amount - orderDetailDtos.ProductAmmount;

                _context.Products.Attach(sp);
                _context.Entry(sp).Property(x => x.Amount).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
