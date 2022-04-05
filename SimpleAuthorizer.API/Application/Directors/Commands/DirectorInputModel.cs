namespace SimpleAuthorizer.API.Application.Directors.Commands
{
    public class DirectorInputModel
    {
        public string Name { get; set; } = default!;

        public DateTime BirthDate { get; set; } = default!;
    }
}
