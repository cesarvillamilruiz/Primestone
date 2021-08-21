using System.ComponentModel.DataAnnotations;

namespace Logic_Layer.DTO
{
    public class AddAddressDTO
    {
        [Required]
        public string TextAddress { get; set; }
        [Required]
        public int AddressTypeId { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
