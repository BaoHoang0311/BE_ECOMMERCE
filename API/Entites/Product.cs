

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
        [StringLength(10, MinimumLength = 3)]
        public string FullName { get; set; }
        public string productOwner { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Amount { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [StringLength(10)]
        public string ModifiedBy { get; set; }

    }
}
