using API.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entites
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new();
        }
        [Key]
        public string Id { get; set; }

        public string OrderNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }



        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer customer { get; set; }



        public List<OrderDetail> OrderDetails { get; set; }
    }
}
