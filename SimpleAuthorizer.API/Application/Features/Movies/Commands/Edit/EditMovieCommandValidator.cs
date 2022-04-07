using FluentValidation;
using SimpleAuthorizer.API.Infrastructure;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands.Edit
{
    public class EditMovieCommandValidator : AbstractValidator<EditMovieCommand>
    {
        public EditMovieCommandValidator(ApiDbContext dbContext)
        {
            this.RuleFor(x => x.Id).NotEmpty();

            this.Include(new MovieCommonInputModelValidator(dbContext));
        }
    }
}
