using AutoMapper;
using MediatR;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleAuthorizer.API.Application.Features.Directors.Queries.List
{
    public class ListDirectorQuery : IRequest<Result<IEnumerable<DirectorCommonOutputModel>>>
    {
        public class ListDirectorQueryHandler 
            : IRequestHandler<ListDirectorQuery, Result<IEnumerable<DirectorCommonOutputModel>>>
        {
            private readonly ApiDbContext _dbContext;
            private readonly IMapper _mapper;

            public ListDirectorQueryHandler(ApiDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<DirectorCommonOutputModel>>> Handle(ListDirectorQuery request, CancellationToken cancellationToken)
            {
                return await this._mapper.ProjectTo<DirectorCommonOutputModel>(
                        this._dbContext.Directors)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
