

using API.Repository;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entites
{
    public class Product : IEntityID
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string productOwner { get; set; }
        public int Amount { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
