using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Domain.Models;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands.Create
{
    public class CreateMovieCommand : MovieCommonInputModel, IRequest<Result<int>>
    {
        public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Result<int>>
        {
            private readonly ApiDbContext _dbContext;

            public CreateMovieCommandHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Result<int>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
            {
                var director = await this._dbContext.Directors
                    .FirstOrDefaultAsync(x => x.Id == request.DirectorId, cancellationToken);

                var movie = new Movie(
                    request.Title,
                    request.ReleaseDate,
                    request.Rating,
                    director!);

                await this._dbContext.AddAsync(movie, cancellationToken);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return movie.Id;
            }
        }
    }
}
