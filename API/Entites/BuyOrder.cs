using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entites
{
    public class BuyOrder
    {
        public BuyOrder()
        {
            BuyOrderDetails = new();
        }
        [Key]
        public int Id { get; set; }

        public string OrderNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }



        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer customer { get; set; }


        public List<BuyOrderDetail> BuyOrderDetails { get; set; }
    }
}
