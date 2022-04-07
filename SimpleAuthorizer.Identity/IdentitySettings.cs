namespace SimpleAuthorizer.Identity
{
    public class IdentitySettings
    {
        public string SuperAdminEmail { get; set; } = default!;

        public string SuperAdminPassword { get; set; } = default!;

        public string AdminEmail { get; set; } = default!;

        public string AdminPassword { get; set; } = default!;
    }
}
