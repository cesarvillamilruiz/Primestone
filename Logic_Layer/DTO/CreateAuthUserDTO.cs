using System.ComponentModel.DataAnnotations;

namespace Logic_Layer.DTO
{
    public class CreateAuthUserDTO
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
