using Microsoft.AspNetCore.Mvc;
using SimpleAuthorizer.API.Application.Directors.Commands.Create;
using SimpleAuthorizer.API.Application.Directors.Commands.Delete;
using SimpleAuthorizer.API.Application.Directors.Commands.Edit;
using SimpleAuthorizer.API.Application.Directors.Queries;
using SimpleAuthorizer.API.Application.Directors.Queries.Get;
using SimpleAuthorizer.API.Application.Directors.Queries.List;
using SimpleAuthorizer.Common.Web;

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
        public async Task<ActionResult<DirectorQueryOutputModel>> Get(
                [FromRoute] GetDirectorQuery query)
                => await this.Send(query);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DirectorQueryOutputModel>>> List()
                => await this.Send(new ListDirectorQuery());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateDirectorCommand command)
            => await this.Send(command);


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Edit(
            [FromBody] EditDirectorCommand command)
                => await this.Send(command);

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteDirectorCommand command)
                => await this.Send(command);
    }
}
