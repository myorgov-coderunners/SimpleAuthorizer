using FluentValidation;
using SimpleAuthorizer.API.Infrastructure;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands.Create
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator(ApiDbContext dbContext)
        {
            this.Include(new MovieCommonInputModelValidator(dbContext));
        }
    }
}
