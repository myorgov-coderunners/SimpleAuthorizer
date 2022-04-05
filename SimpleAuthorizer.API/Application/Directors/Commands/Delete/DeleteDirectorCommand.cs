using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Web;

namespace SimpleAuthorizer.API.Application.Directors.Commands.Delete
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
                        .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (director == null)
                {
                    throw new Exception($"No such director with id:{request.Id}");
                }

                this._dbContext.Remove(director);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
