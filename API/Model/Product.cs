using System;

namespace API.Model
{
    public class Product
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public string productOwner { get; set; }
        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
