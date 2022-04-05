using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Web;

namespace SimpleAuthorizer.API.Application.Directors.Commands.Edit
{
    public class EditDirectorCommand : DirectorInputModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditDirectorCommandHandler : IRequestHandler<EditDirectorCommand, Result>
        {
            private readonly ApiDbContext _dbContext;

            public EditDirectorCommandHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Result> Handle(EditDirectorCommand request, CancellationToken cancellationToken)
            {
                var director = await this._dbContext.Directors
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (director == null)
                {
                    throw new Exception($"No such director with id:{request.Id}");
                }

                director
                    .UpdateName(request.Name)
                    .UpdateBirthDate(request.BirthDate);

                this._dbContext.Update(director);
                await this._dbContext.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
        }
    }
}
