using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerDtos
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "nhap dung format email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "nhap dung formatPhone")]
        public string Phone { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Address { get; set; }
    }
}
