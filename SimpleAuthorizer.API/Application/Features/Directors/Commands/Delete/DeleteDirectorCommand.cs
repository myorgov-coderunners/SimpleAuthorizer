using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Directors.Commands.Delete
{
    public class DeleteDirectorCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, Result>
        {
            private readonly ApiDbContext _dbContext;

            public DeleteDirectorCommandHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Result> Handle(
                DeleteDirectorCommand request, 
                CancellationToken cancellationToken)
            {
                var director = await this._dbContext.Directors
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (director == null)
                {
                    return $"There is no director with this id:{request.Id}";
                }

                this._dbContext.Remove(director);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
