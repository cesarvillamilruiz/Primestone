using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Logic_Layer.DTO
{
    public class StudentCourseDTO
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
