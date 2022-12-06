using API.Entites;
using System.Collections.Generic;

namespace API.Dtos
{
    public class OrderDtos
    {
        public string OrderNo { get; set; }
        public string CustomerId { get; set; }
        public List<OrderDetailDtos> orderDetailDtos { get; set; }
    }
}
