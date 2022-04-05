using FluentValidation;

namespace SimpleAuthorizer.API.Application.Directors.Commands
{
    public class DirectorInputModelValidator : AbstractValidator<DirectorInputModel>
    {
        public DirectorInputModelValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}
