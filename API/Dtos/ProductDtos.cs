using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductDtos
    {
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
