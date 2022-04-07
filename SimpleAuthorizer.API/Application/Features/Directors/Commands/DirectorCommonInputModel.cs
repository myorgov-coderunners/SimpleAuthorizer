namespace SimpleAuthorizer.API.Application.Features.Directors.Commands
{
    public class DirectorCommonInputModel
    {
        public string Name { get; set; } = default!;

        public DateTime BirthDate { get; set; } = default!;
    }
}
