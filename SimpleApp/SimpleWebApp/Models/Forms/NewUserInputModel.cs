using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.Models.Forms
{
    public class NewUserInputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;


        [Required, MaxLength(500)]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
