using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerDtos
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
