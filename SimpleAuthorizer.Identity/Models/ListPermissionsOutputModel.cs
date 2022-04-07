namespace SimpleAuthorizer.Identity.Models
{
    public class ListPermissionsOutputModel
    {
        public IEnumerable<string> Permissions { get; set; } = default!;

        public IEnumerable<string> Roles { get; set; } = default!;
    }
}
