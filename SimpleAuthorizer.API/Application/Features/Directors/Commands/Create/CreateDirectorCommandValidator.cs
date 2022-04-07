using FluentValidation;

namespace SimpleAuthorizer.API.Application.Features.Directors.Commands.Create
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            this.Include(new DirectorCommonInputModelValidator());
        }
    }
}
