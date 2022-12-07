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
        [Required]
        public string FullName { get; set; }
        [Required]
        public int Amount { get; set; }

    }
}
