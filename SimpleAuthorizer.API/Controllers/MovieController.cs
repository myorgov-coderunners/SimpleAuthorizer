using Microsoft.AspNetCore.Mvc;
using SimpleAuthorizer.API.Application.Features.Movies.Commands.Create;
using SimpleAuthorizer.API.Application.Features.Movies.Commands.Delete;
using SimpleAuthorizer.API.Application.Features.Movies.Commands.Edit;
using SimpleAuthorizer.API.Application.Features.Movies.Queries;
using SimpleAuthorizer.API.Application.Features.Movies.Queries.Get;
using SimpleAuthorizer.API.Application.Features.Movies.Queries.List;
using SimpleAuthorizer.Common.Controllers;

namespace SimpleAuthorizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Route("{id:int}")]
        public async Task<ActionResult<MovieCommonOutputModel>> Get(
            [FromRoute] GetMovieQuery query)
            => await this.Send(query);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieCommonOutputModel>>> List(
            [FromRoute] ListMovieQuery query)
            => await this.Send(query);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateMovieCommand command)
            => await this.Send(command);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Edit(
            [FromBody] EditMovieCommand command)
                => await this.Send(command);

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteMovieCommand command)
                => await this.Send(command);
    }
}
