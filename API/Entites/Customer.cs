using API.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Entites
{
    public class Customer : IEntityID
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "nhap dung format email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10}$", ErrorMessage = "Please enter valid phone no.")]
        [Phone(ErrorMessage = "nhap dung formatPhone")]
        public string Phone { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Address { get; set; }

        [StringLength(1000)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [StringLength(1000)]
        public string ModifiedBy { get; set; }

    }
}
