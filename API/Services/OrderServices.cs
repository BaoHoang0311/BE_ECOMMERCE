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
    public class OrderServices : IOrderRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public OrderServices(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderDtos orderDtos)
        {
            var cus = _context.Customers.FirstOrDefaultAsync(o=>o.Id ==orderDtos.CustomerId);
            if(cus!= null)
            {
                var order = _mapper.Map<Order>(orderDtos);
                order.Id = Guid.NewGuid().ToString();

                order.CreatedBy = "admin";
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;
                order.ModifiedBy = "admin";

                order.TotalPrice = 0;
                if (orderDtos.orderDetailDtos.Count > 0)
                {
                    order.TotalPrice = orderDtos.orderDetailDtos.Aggregate(order.TotalPrice, (a, b) => a + (b.Price * b.ProductAmmount));
                }
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                if (orderDtos.orderDetailDtos.Count > 0)
                {
                    foreach (var item in orderDtos.orderDetailDtos)
                    {
                        if (checkammount(item) == true && UpdateAmmount(item))
                        {
                            var orderDetail = _mapper.Map<OrderDetail>(item);

                            orderDetail.Id = Guid.NewGuid().ToString();

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
            }
            else
            {

            }

        }

        public Task<Order> GetCusOrder()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Order>> GetListOrder()
        {

            return null;
        }

        public async Task<Order> GetOrderbyIdCus(string CusId)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(x => x.Id == CusId);
            if(cus!= null)
            {
                var listorder = await _context.Orders.Include(o => o.customer)
                                                    .Include(o => o.OrderDetails)
                                                    .FirstOrDefaultAsync(o=>o.CustomerId == cus.Id);
                return listorder;
            }
            return null;
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
                sp.Amount =sp.Amount - orderDetailDtos.ProductAmmount;

                _context.Products.Attach(sp);
                _context.Entry(sp).Property(x => x.Amount).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
