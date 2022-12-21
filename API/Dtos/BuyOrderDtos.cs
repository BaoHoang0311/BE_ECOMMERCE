using API.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BuyOrderDtos
    {
        public int BuyOrderId { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        public decimal TotalPrice { get; set; }
        [ValidationListEmptyBuyOrderDetail]
        public List<BuyOrderDetailDtos> BuyorderDetailDtos { get; set; }
    }
}
