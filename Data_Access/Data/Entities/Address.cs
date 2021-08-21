using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class Address:IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string TextAddress { get; set; }
        [Required]
        public AddressType AddressType { get; set; }
        public int AddressTypeId { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
