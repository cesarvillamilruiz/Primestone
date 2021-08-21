using System.ComponentModel.DataAnnotations;

namespace Logic_Layer.DTO
{
    public class LoginAuthUserDTO
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
