using MediatR;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Web;
using Microsoft.EntityFrameworkCore;

namespace SimpleAuthorizer.API.Application.Directors.Queries.Get
{
    public class GetDirectorQuery : IRequest<Result<DirectorQueryOutputModel>>
    {
        public int Id { get; set; }

        public class GetDirectorQueryHandler
            : IRequestHandler<GetDirectorQuery, Result<DirectorQueryOutputModel>>
        {
            private readonly ApiDbContext _dbContext;

            public GetDirectorQueryHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Result<DirectorQueryOutputModel>> Handle(
                GetDirectorQuery request, 
                CancellationToken cancellationToken)
            {
                var director = await this._dbContext.Directors
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (director == null)
                {
                    throw new Exception($"No such director with id:{request.Id}");
                }

                /// To do: Add Automapper
                return new DirectorQueryOutputModel()
                {
                    Id = director.Id,
                    BirthDate = director.BirthDate,
                    Movies = director.Movies.Select(movie => new DirectorQueryMovieOutputModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        Rating = (int)movie.Rating
                    })
                };
            }
        }
    }
}
