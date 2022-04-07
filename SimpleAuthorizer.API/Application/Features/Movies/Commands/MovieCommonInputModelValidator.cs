using FluentValidation;
using SimpleAuthorizer.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands
{
    public class MovieCommonInputModelValidator : AbstractValidator<MovieCommonInputModel>
    {
        public MovieCommonInputModelValidator(ApiDbContext dbContext)
        {
            this.RuleFor(x => x.Title).NotEmpty();

            this.RuleFor(x => x.DirectorId)
                .NotEmpty()
                .WithMessage("DirectorId is required");

            this.RuleFor(x => x.DirectorId)
                .MustAsync(async (directorID, token)
                    => await dbContext.Directors.AnyAsync(x => x.Id == directorID, token))
                .WithMessage("There is no director with this id:{value}");

            this.RuleFor(x => x.ReleaseDate).NotEmpty();
            this.RuleFor(x => x.Rating).IsInEnum();
        }
    }
}
