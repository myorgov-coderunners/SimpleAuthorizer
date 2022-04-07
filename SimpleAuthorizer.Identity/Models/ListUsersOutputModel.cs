namespace SimpleAuthorizer.Identity.Models
{
    public class ListUsersOutputModel
    {
        public string UserId { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public IEnumerable<string> Roles { get; set; } = default!;
    }
}
