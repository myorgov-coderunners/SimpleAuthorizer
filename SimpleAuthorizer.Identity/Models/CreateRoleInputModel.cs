using System.ComponentModel.DataAnnotations;

namespace SimpleAuthorizer.Identity.Models
{
    public class CreateRoleInputModel
    {
        [Required]
        public string RoleName { get; set; } = default!;

        [Required]
        public List<string> Permissions { get; set; } = default!;
    }
}
