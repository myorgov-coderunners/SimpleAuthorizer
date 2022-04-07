using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Movies.Queries.List
{
    public class ListMovieQuery : IRequest<Result<IEnumerable<MovieCommonOutputModel>>>
    {
        public class ListMovieQueryHandler 
            : IRequestHandler<ListMovieQuery, Result<IEnumerable<MovieCommonOutputModel>>>
        {
            private readonly ApiDbContext _dbContext;
            private readonly IMapper _mapper;

            public ListMovieQueryHandler(ApiDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<MovieCommonOutputModel>>> Handle(ListMovieQuery request, CancellationToken cancellationToken)
            {
                return await this._mapper.ProjectTo<MovieCommonOutputModel>(
                            this._dbContext.Movies)
                        .ToListAsync(cancellationToken);
            }
        }
    }
}
