using Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entites
{
    public class Product : IEntityID
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string productOwner { get; set; }
        public string PictureUrl { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }



        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
