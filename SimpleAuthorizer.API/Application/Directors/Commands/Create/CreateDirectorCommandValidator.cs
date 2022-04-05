using FluentValidation;

namespace SimpleAuthorizer.API.Application.Directors.Commands.Create
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            this.Include(new DirectorInputModelValidator());
        }
    }
}
