using System;
using System.ComponentModel.DataAnnotations;

namespace Logic_Layer.DTO
{
    public class StudentDTO
    {
        [Required]
        [MaxLength(100)]
        public string Names { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastNames { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
    }
}
