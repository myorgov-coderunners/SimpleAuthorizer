using FluentValidation;

namespace SimpleAuthorizer.API.Application.Features.Directors.Commands
{
    public class DirectorCommonInputModelValidator : AbstractValidator<DirectorCommonInputModel>
    {
        public DirectorCommonInputModelValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}
