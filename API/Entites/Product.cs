

using API.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entites
{
    public class Product : IEntityID
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string productOwner { get; set; }
        [Required]  
        public int Amount { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
