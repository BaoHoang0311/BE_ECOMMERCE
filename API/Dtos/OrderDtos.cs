using API.Entites;
using System.Collections.Generic;

namespace API.Dtos
{
    public class OrderDtos
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDtos> orderDetailDtos { get; set; }
    }
}
