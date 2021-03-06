using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Application.Features.Directors.Commands.Edit
{
    public class EditDirectorCommand : DirectorCommonInputModel, IRequest<Result>
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
                    return $"There is no director with this id:{request.Id}";
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
