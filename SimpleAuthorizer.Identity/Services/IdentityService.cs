using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleAuthorizer.Common;
using SimpleAuthorizer.Common.Models;
using SimpleAuthorizer.Identity.Infrastructure;
using SimpleAuthorizer.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleAuthorizer.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private const string InvalidErrorMessage = "Invalid credentials.";


        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SimpleIdentityDbContext _dbContext;

        public IdentityService(
            UserManager<IdentityUser> userManager,
            IOptions<JwtTokenSettings> jwtSettings,
            RoleManager<IdentityRole> roleManager, 
            SimpleIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }


        public async Task<Result<string>> Login(LoginUserInputModel input)
        {
            var user = await this._userManager.FindByNameAsync(input.Email);
            if (user == null)
            {
                return Result<string>.Failure(new List<string> { InvalidErrorMessage });
            }

            var passwordValid = await this._userManager.CheckPasswordAsync(user, input.Password);
            if (!passwordValid)
            {
                return Result<string>.Failure(new List<string> { InvalidErrorMessage });
            }

            var roles = await this._userManager.GetRolesAsync(user);

            var userClaims = new List<Claim>();
            foreach (var role in roles)
            {
                var identityRole = await this._roleManager.Roles.FirstAsync(c => c.Name == role);

                var claims = await this._roleManager.GetClaimsAsync(identityRole);
                userClaims.AddRange(claims);
            }

            var token = GenerateTokenAsync(user, userClaims);

            return Result<string>.SuccessWith(token);
        }

        public async Task<Result> CreateUser(CreateUserInputModel input)
        {
            var user = new IdentityUser(input.Email);

            var identityResult = await this._userManager.CreateAsync(user, input.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            foreach (var role in input.Roles)
            {
                await this._userManager.AddToRoleAsync(user, role);
            }

            return identityResult.Succeeded
                ? Result.Success
                : Result.Failure(errors);
        }

        public async Task<Result> EditUserRole(EditUserRoleUnputModel input)
        {
            var user = await this._userManager.FindByIdAsync(input.UserId);

            //There is a much better solution, but I had to extend the Identity,
            //I have no time left, so I leave it like that

            var currentRoles = await this._userManager.GetRolesAsync(user);
            await this._userManager.RemoveFromRolesAsync(user, currentRoles);

            await this._userManager.AddToRolesAsync(user, input.Roles);

            return Result.Success;
        }

        public async Task<Result<ListPermissionsOutputModel>> ListPermissions()
        {
            var result = new ListPermissionsOutputModel();

            result.Permissions =  await this._dbContext.RoleClaims
                .Select(x => x.ClaimValue)
                .Distinct()
                .ToListAsync();

            result.Roles = await this._roleManager.Roles
                .Select(x => x.Name)
                .ToListAsync();

            return result;
        }

        public async Task<Result> CreateRole(CreateRoleInputModel input)
        {
            if (await ValidatePermissions(input.Permissions))
            {
                return "Some of the permissions are not supported ";
            }
            var newRole = new IdentityRole(input.RoleName);
            await this._roleManager.CreateAsync(newRole);

            foreach (var permission in input.Permissions)
            {
                await this._roleManager.AddClaimAsync(newRole, new Claim("Permission", permission));
            }

            return Result.Success;
        }

        public async Task<Result<IEnumerable<ListUsersOutputModel>>> ListUsers()
        {
            //There is a much better solution, but I had to extend the Identity,
            //I have no time left, so I leave it like that

            var usersRoles = await this._dbContext.UserRoles.ToListAsync();
            var users = await this._dbContext.Users.ToListAsync();
            var roles = await this._dbContext.Roles.ToListAsync();

            var result = new List<ListUsersOutputModel>();

            foreach (var user in users)
            {
                var userRoles = usersRoles
                    .Where(x => x.UserId == user.Id)
                    .Select(x => roles.First(r => x.RoleId == r.Id).Name);

                result.Add(new ListUsersOutputModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = userRoles
                });
            }

            return result;
        }

        private string GenerateTokenAsync(IdentityUser user, List<Claim> roleClaims)
        {
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim("Department", "HR") 
            }.Concat(roleClaims);

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key)
            );
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<bool> ValidatePermissions(List<string> newPermission)
        {
            var roleClaims = await this._dbContext.RoleClaims
                .Select(x => x.ClaimValue).Distinct()
                .ToListAsync();

            return !newPermission.All(c => roleClaims.Contains(c));
        }

    }
}
