namespace SimpleAuthorizer.API.Application.Directors.Queries
{
    public class DirectorQueryOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public IEnumerable<DirectorQueryMovieOutputModel> Movies { get; set; } = 
            new List<DirectorQueryMovieOutputModel>();
    }

    public class DirectorQueryMovieOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }
    }
}
