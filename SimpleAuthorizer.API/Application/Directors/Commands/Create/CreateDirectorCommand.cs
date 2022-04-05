﻿using MediatR;
using SimpleAuthorizer.API.Domain.Models;
using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common.Web;

namespace SimpleAuthorizer.API.Application.Directors.Commands.Create
{
    public class CreateDirectorCommand : DirectorInputModel, IRequest<Result<int>> 
    {
        public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, Result<int>>
        {

            private readonly ApiDbContext _dbContext;

            public CreateDirectorCommandHandler(ApiDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Result<int>> Handle(
                CreateDirectorCommand request, 
                CancellationToken cancellationToken)
            {
                var director = new Director(request.Name, request.BirthDate);

                await this._dbContext.AddAsync(director, cancellationToken);
                await this._dbContext.SaveChangesAsync(cancellationToken);

                return director.Id;
            }
        }
    }
}
