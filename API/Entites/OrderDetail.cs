using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace API.Entites
{
    public class OrderDetail
    {
        public string Id { get; set; }

        public string OrderNo { get; set; }
        public int ammount { get; set; }
        public int price { get; set; }
        public decimal TotalPrice { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
