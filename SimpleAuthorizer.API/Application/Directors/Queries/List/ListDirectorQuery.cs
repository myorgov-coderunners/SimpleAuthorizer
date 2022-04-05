using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Web;

namespace SimpleAuthorizer.API.Application.Directors.Queries.List
{
    public class ListDirectorQuery : IRequest<Result<List<DirectorQueryOutputModel>>>
    {
        public class ListDirectorQueryHandler 
            : IRequestHandler<ListDirectorQuery, Result<List<DirectorQueryOutputModel>>>
        {
            private readonly ApiDbContext _dbContext;

            public ListDirectorQueryHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Result<List<DirectorQueryOutputModel>>> Handle(ListDirectorQuery request, CancellationToken cancellationToken)
            {
                var directors = await this._dbContext.Directors.ToListAsync(cancellationToken);

                /// To do: Add Automapper
                var result = new List<DirectorQueryOutputModel>();
                foreach (var director in directors)
                {
                    result.Add(new DirectorQueryOutputModel()
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
                    });
                }

                return result;
            }
        }
    }
}
