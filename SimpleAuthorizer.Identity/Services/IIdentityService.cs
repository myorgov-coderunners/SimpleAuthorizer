using SimpleAuthorizer.Common.Models;
using SimpleAuthorizer.Identity.Models;

namespace SimpleAuthorizer.Identity.Services
{
    public interface IIdentityService
    {
        Task<Result<string>> Login(LoginUserInputModel input);

        Task<Result> CreateUser(CreateUserInputModel input);

        Task<Result<IEnumerable<ListUsersOutputModel>>> ListUsers();

        Task<Result<ListPermissionsOutputModel>> ListPermissions();

        Task<Result> CreateRole(CreateRoleInputModel input);

        Task<Result> EditUserRole(EditUserRoleUnputModel input);
    }
}
