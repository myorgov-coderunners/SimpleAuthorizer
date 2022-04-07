using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands.Delete
{
    public class DeleteMovieCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Result>
        {
            private readonly ApiDbContext _dbContext;

            public DeleteMovieCommandHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Result> Handle(
                DeleteMovieCommand request, 
                CancellationToken cancellationToken)
            {
                var movie = await this._dbContext.Movies
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (movie == null)
                {
                    return $"There is no movie with this id:{request.Id}";
                }

                this._dbContext.Remove(movie);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
