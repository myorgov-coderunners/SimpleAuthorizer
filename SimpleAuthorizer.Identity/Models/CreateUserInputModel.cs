using System.ComponentModel.DataAnnotations;

namespace SimpleAuthorizer.Identity.Models
{
    public class CreateUserInputModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        public IEnumerable<string> Roles { get; set; } = default!;
    }
}
