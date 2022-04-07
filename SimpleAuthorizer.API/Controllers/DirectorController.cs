using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthorizer.API.Application.Features.Directors.Commands.Create;
using SimpleAuthorizer.API.Application.Features.Directors.Commands.Delete;
using SimpleAuthorizer.API.Application.Features.Directors.Commands.Edit;
using SimpleAuthorizer.API.Application.Features.Directors.Queries;
using SimpleAuthorizer.API.Application.Features.Directors.Queries.Get;
using SimpleAuthorizer.API.Application.Features.Directors.Queries.List;
using SimpleAuthorizer.Common.Controllers;
using static SimpleAuthorizer.Common.IdentityConstants;

namespace SimpleAuthorizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // To do: Add authorization attributes and middleware
    public class DirectorController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Route("{id:int}")]
        [Authorize(Policy = CustomClaims.DirectorRead)]
        public async Task<ActionResult<DirectorCommonOutputModel>> Get(
                [FromRoute] GetDirectorQuery query)
                => await this.Send(query);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = CustomClaims.DirectorRead)]
        public async Task<ActionResult<IEnumerable<DirectorCommonOutputModel>>> List()
                => await this.Send(new ListDirectorQuery());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = CustomClaims.DirectorCreate)]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateDirectorCommand command)
            => await this.Send(command);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Authorize(Policy = CustomClaims.DirectorWrite)]
        public async Task<ActionResult> Edit(
            [FromBody] EditDirectorCommand command)
                => await this.Send(command);

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Route("{id:int}")]
        [Authorize(Policy = CustomClaims.DirectorDelete)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteDirectorCommand command)
                => await this.Send(command);
    }
}
