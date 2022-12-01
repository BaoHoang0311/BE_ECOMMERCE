using Core.Interfaces;
using System;

namespace Core.Entites
{
    public class Product : IEntityID
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string productOwner { get; set; }
        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
