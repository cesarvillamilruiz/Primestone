using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class Student:IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Names { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string LastNames { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public IEnumerable<Address> Direcciones { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
