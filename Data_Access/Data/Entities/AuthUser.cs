using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class AuthUser : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string User { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Password { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
