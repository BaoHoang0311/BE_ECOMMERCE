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
using NuGet.Versioning;
using Microsoft.AspNetCore.Razor.Language;
using API.Helpers;
using API.Repository;

namespace API.Services
{
    public class OrderServices : EntityBaseRepository<Order>, IOrderServices
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public OrderServices(MyDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        // BE tính 
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

                order.TotalPrice = TotalPrice(order.OrderDetails);

                if (order.TotalPrice > 0)
                {
                    var list = order.OrderDetails;
                    order.OrderDetails = null;

                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();


                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (checkammount(item) == true && UpdateAmmount(item))
                            {
                                var orderDetail = _mapper.Map<OrderDetail>(item);

                                orderDetail.OrderNo = "";

                                orderDetail.CreatedBy = "";
                                orderDetail.CreatedDate = DateTime.Now;
                                orderDetail.ModifiedDate = DateTime.Now;
                                orderDetail.OrderId = order.Id;

                                orderDetail.TotalPrice = item.price * item.ammount;

                                await _context.OrderDetails.AddAsync(orderDetail);

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

        private decimal TotalPrice(List<OrderDetail> orderDetail)
        {
            decimal res = 0;
            if (orderDetail.Count > 0)
            {
                foreach (var item in orderDetail)
                {
                    if (checkammount(item) == true)
                    {
                        res += item.ammount * item.price;
                    }
                }
            }
            return res;
        }
        private bool checkammount(OrderDetail orderDetail)
        {
            var sp = _context.Products.FirstOrDefault(x => x.Id == orderDetail.ProductId);

            if (sp != null && sp.Amount >= orderDetail.ammount)
            {
                return true;
            }
            return false;
        }

        private bool UpdateAmmount(OrderDetail orderDetail)
        {
            var sp = _context.Products.FirstOrDefault(x => x.Id == orderDetail.ProductId);

            if (sp != null)
            {
                sp.Amount = sp.Amount - orderDetail.ammount;

                _context.Products.Attach(sp);
                _context.Entry(sp).Property(x => x.Amount).IsModified = true;
                //dasdas
                _context.SaveChanges();
                return true;
            }
            return false;
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

        // FE xử lý
        public async Task<bool> AddOrderAsync_1(OrderDtos orderDtos)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(o => o.Id == orderDtos.CustomerId);
            if (cus != null)
            {
                var order = _mapper.Map<Order>(orderDtos);

                order.CreatedBy = "admin";
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;
                order.ModifiedBy = "admin";

                //order.TotalPrice = TotalPrice(order.OrderDetails);

                if (order.TotalPrice > 0)
                {
                    var list = order.OrderDetails;
                    order.OrderDetails = null;

                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();


                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (/*checkammount(item) == true && */ UpdateAmmount(item))
                            {
                                var orderDetail = _mapper.Map<OrderDetail>(item);

                                orderDetail.OrderNo = "";

                                orderDetail.CreatedBy = "";
                                orderDetail.CreatedDate = DateTime.Now;
                                orderDetail.ModifiedDate = DateTime.Now;
                                orderDetail.OrderId = order.Id;

                                //orderDetail.TotalPrice = item.price * item.ammount;

                                await _context.OrderDetails.AddAsync(orderDetail);

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
