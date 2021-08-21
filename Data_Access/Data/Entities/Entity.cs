using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class DoWork:IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool EstaBorrado { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Evento { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}
