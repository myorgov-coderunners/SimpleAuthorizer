using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands.Edit
{
    public class EditMovieCommand : MovieCommonInputModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditMovieCommandHandler : IRequestHandler<EditMovieCommand, Result>
        {
            private readonly ApiDbContext _dbContext;

            public EditMovieCommandHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Result> Handle(EditMovieCommand request, CancellationToken cancellationToken)
            {
                var movie = await this._dbContext.Movies
                    .Include(x => x.Director)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (movie == null)
                {
                    return $"There is no movie with this id:{request.Id}";
                }

                movie
                  .UpdateRating(request.Rating)
                  .UpdateTitle(request.Title)
                  .UpdateReleaseDate(request.ReleaseDate);

                if (movie.Director.Id != request.DirectorId)
                {
                    var director = this._dbContext.Directors
                        .FirstOrDefault(x => x.Id == request.DirectorId);

                    movie.UpdateDirector(director!);
                }

                this._dbContext.Update(movie);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
