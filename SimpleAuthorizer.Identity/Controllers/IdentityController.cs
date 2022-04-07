using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthorizer.Identity.Models;
using SimpleAuthorizer.Identity.Services;
using static SimpleAuthorizer.Common.IdentityConstants;

namespace SimpleAuthorizer.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult> Login(LoginUserInputModel input)
        {
            var result = await this._identityService.Login(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }


        [HttpPost]
        [Route(nameof(CreateUser))]
        [Authorize(Policy = CustomClaims.Admin)]
        public async Task<ActionResult> CreateUser(CreateUserInputModel input)
        {
            var result = await this._identityService.CreateUser(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }


        [HttpPost]
        [Route(nameof(CreateRole))]
        [Authorize(Policy = CustomClaims.Admin)]
        public async Task<ActionResult> CreateRole(CreateRoleInputModel input)
        {
            var result = await this._identityService.CreateRole(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }


        [HttpPost]
        [Route(nameof(EditUserRoles))]
        [Authorize(Policy = CustomClaims.SuperAdmin)]
        public async Task<ActionResult> EditUserRoles(EditUserRoleUnputModel input)
        {
            var result = await this._identityService.EditUserRole(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet]
        [Route(nameof(ListUsers))]
        [Authorize(Policy = CustomClaims.Admin)]
        public async Task<ActionResult> ListUsers()
        {
            var result = await this._identityService.ListUsers();

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(ListPermissions))]
        [Authorize(Policy = CustomClaims.Admin)]
        public async Task<ActionResult> ListPermissions()
        {
            var result = await this._identityService.ListPermissions();

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }



    }
}
