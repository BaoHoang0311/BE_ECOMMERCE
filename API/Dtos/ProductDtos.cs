using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductDtos
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string FullName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Amount { get; set; }

    }
}
