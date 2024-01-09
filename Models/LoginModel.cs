using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
