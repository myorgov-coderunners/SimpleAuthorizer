using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Movies.Queries.Get
{
    public class GetMovieQuery : IRequest<Result<MovieCommonOutputModel>>
    {
        public int Id { get; set; }

        public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, Result<MovieCommonOutputModel>>
        {
            private readonly ApiDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetMovieQueryHandler(ApiDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<Result<MovieCommonOutputModel>> Handle(
                GetMovieQuery request, 
                CancellationToken cancellationToken)
            {
                return await this._mapper.ProjectTo<MovieCommonOutputModel>(
                        this._dbContext.Movies.Where(x => x.Id == request.Id))
                    .FirstOrDefaultAsync(cancellationToken) ?? new MovieCommonOutputModel();
            }
        }
    }
}
