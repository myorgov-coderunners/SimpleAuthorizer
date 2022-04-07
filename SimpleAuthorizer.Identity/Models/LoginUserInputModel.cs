using System.ComponentModel.DataAnnotations;

namespace SimpleAuthorizer.Identity.Models
{
    public class LoginUserInputModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
