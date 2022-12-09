using API.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace API.Entites
{
    public class OrderDetail : IEntityID
    {
        [Key]
        public int Id { get; set; }

        public string OrderNo { get; set; }
        public int ammount { get; set; }
        public int price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public int OrderId { get; set; } 
        [ForeignKey("OrderId")]
        public  Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
