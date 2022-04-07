using SimpleAuthorizer.Common.Models;

namespace SimpleAuthorizer.API.Domain.Models
{
    public class Director : Entity<int>
    {
        private readonly HashSet<Movie> _movies = new HashSet<Movie>();

        public Director(
            string name, 
            DateTime birthDate)
        {
            this.Name = name;
            this.BirthDate = birthDate;
        }

        private Director()
        {
            this.Name = default!;
        }
        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public IReadOnlyCollection<Movie> Movies => this._movies.ToList().AsReadOnly();

        public Director UpdateName(string name)
        {
            this.Name = name;
            return this;
        }

        public Director UpdateBirthDate(DateTime birthDate)
        {
            this.BirthDate = birthDate;
            return this;
        }

        private void Validate()
        {
            // Validation can be added here
            // For the constructor and the methods
        }

    }
}
