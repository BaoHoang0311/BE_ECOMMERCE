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
        public string FullName { get; set; }
        public int Amount { get; set; }

    }
}
