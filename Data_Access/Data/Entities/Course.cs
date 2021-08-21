using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class Course:IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string CodigoCurso { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string NombreCurso { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
