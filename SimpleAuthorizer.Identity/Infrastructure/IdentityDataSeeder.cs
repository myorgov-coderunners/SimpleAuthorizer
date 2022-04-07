using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SimpleAuthorizer.Common.Services;
using System.Security.Claims;
using static SimpleAuthorizer.Common.IdentityConstants;

namespace SimpleAuthorizer.Identity.Infrastructure
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentitySettings _identitySettings;

        public IdentityDataSeeder(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentitySettings> identitySettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identitySettings = identitySettings.Value;
        }
        public void SeedData()
        {
            if (!this._roleManager.Roles.Any())
            {
                Task
                    .Run(async () =>
                    {
                        var supperAdminRole = new IdentityRole(DefaultRoles.CodeChallengeSuperAdmin);
                        var adminRole = new IdentityRole(DefaultRoles.CodeChallengeAdmin);
                        var codeChallengeReadRole = new IdentityRole(DefaultRoles.CodeChallengeRead);
                        var codeChallengeWriteRole = new IdentityRole(DefaultRoles.CodeChallengeWrite);
                        var codeChallengeDeleteRole = new IdentityRole(DefaultRoles.CodeChallengeDelete);


                        await this._roleManager.CreateAsync(supperAdminRole);
                        await this._roleManager.CreateAsync(adminRole);
                        await this._roleManager.CreateAsync(codeChallengeReadRole);
                        await this._roleManager.CreateAsync(codeChallengeWriteRole);
                        await this._roleManager.CreateAsync(codeChallengeDeleteRole);

                        await this._roleManager.AddClaimAsync(supperAdminRole, new Claim("Permission", CustomClaims.SuperAdmin));
                        await this._roleManager.AddClaimAsync(adminRole, new Claim("Permission", CustomClaims.Admin));

                        await this._roleManager.AddClaimAsync(codeChallengeReadRole, new Claim("Permission", CustomClaims.MovieRead));
                        await this._roleManager.AddClaimAsync(codeChallengeReadRole, new Claim("Permission", CustomClaims.DirectorRead));

                        await this._roleManager.AddClaimAsync(codeChallengeWriteRole, new Claim("Permission", CustomClaims.MovieWrite));
                        await this._roleManager.AddClaimAsync(codeChallengeWriteRole, new Claim("Permission", CustomClaims.DirectorWrite));

                        await this._roleManager.AddClaimAsync(codeChallengeWriteRole, new Claim("Permission", CustomClaims.MovieCreate));
                        await this._roleManager.AddClaimAsync(codeChallengeWriteRole, new Claim("Permission", CustomClaims.DirectorCreate));

                        await this._roleManager.AddClaimAsync(codeChallengeDeleteRole, new Claim("Permission", CustomClaims.MovieDelete));
                        await this._roleManager.AddClaimAsync(codeChallengeDeleteRole, new Claim("Permission", CustomClaims.DirectorDelete));
                    })
                    .GetAwaiter()
                    .GetResult();
            }
            if (!this._userManager.Users.Any())
            {
                Task
                    .Run(async () =>
                    {
                        var superAdmin = new IdentityUser(_identitySettings.SuperAdminEmail);
                        await this._userManager.CreateAsync(superAdmin, this._identitySettings.SuperAdminPassword);

                        var admin = new IdentityUser(this._identitySettings.AdminEmail);
                        await this._userManager.CreateAsync(admin, this._identitySettings.AdminPassword);

                        await this._userManager.AddToRoleAsync(superAdmin, DefaultRoles.CodeChallengeSuperAdmin);
                        await this._userManager.AddToRoleAsync(admin, DefaultRoles.CodeChallengeAdmin);
                    })
                    .GetAwaiter()
                    .GetResult();
            }





                //var adminRole = new IdentityRole("Admin");
                //var supperAdminRole = new IdentityRole("SupperAdmin");
                //await this._roleManager.CreateAsync(adminRole);
                //await this._roleManager.CreateAsync(supperAdminRole);

                //await this._roleManager.AddClaimAsync(adminRole, new Claim("Permission", "projects.view"));
                //await this._roleManager.AddClaimAsync(adminRole, new Claim("Permission", "projects.Edit"));

            //    var adminRole = await _roleManager.FindByNameAsync("Admin");

            //var a = await this._roleManager.GetClaimsAsync(adminRole);
        }
    }
}
