using MediatR;
using SimpleAuthorizer.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.Common.Models;
using AutoMapper;

namespace SimpleAuthorizer.API.Application.Features.Directors.Queries.Get
{
    public class GetDirectorQuery : IRequest<Result<DirectorCommonOutputModel>>
    {
        public int Id { get; set; }

        public class GetDirectorQueryHandler
            : IRequestHandler<GetDirectorQuery, Result<DirectorCommonOutputModel>>
        {
            private readonly ApiDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetDirectorQueryHandler(ApiDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<Result<DirectorCommonOutputModel>> Handle(
                GetDirectorQuery request, 
                CancellationToken cancellationToken)
            {
                return await this._mapper.ProjectTo<DirectorCommonOutputModel>(
                        this._dbContext.Directors.Where(x => x.Id == request.Id))
                    .FirstOrDefaultAsync(cancellationToken) ?? new DirectorCommonOutputModel();
            }
        }
    }
}
