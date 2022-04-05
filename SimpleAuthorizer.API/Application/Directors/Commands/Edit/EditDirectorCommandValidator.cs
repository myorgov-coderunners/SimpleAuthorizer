using FluentValidation;

namespace SimpleAuthorizer.API.Application.Directors.Commands.Edit
{
    public class EditDirectorCommandValidator : AbstractValidator<EditDirectorCommand>
    {
        public EditDirectorCommandValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();

            this.Include(new DirectorInputModelValidator());
        }
        
    }
}
