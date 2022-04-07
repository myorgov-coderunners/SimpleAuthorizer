using System.ComponentModel.DataAnnotations;

namespace SimpleAuthorizer.Identity.Models
{
    public class EditUserRoleUnputModel
    {
        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        public List<string> Roles { get; set; } = default!;
    }
}
