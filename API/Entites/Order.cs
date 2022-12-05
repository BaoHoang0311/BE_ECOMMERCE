using API.Repository;
using System;
using System.Collections.Generic;

namespace API.Entites
{
    public class Order
    {
        public string Id { get; set; }

        public string OrderNo { get; set; }
        public decimal TotalPrice { get; set; }



        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public Customer customer { get; set; }
        public string CustomerId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
