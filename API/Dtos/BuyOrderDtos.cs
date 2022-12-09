using System.Collections.Generic;

namespace API.Dtos
{
    public class BuyOrderDtos
    {
        public int BuyOrderId { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public List<BuyOrderDetailDtos> BuyorderDetailDtos { get; set; }
    }
}
