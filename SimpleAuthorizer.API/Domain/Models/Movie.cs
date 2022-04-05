using SimpleAuthorizer.Common.Domain;

namespace SimpleAuthorizer.API.Domain.Models
{
    public class Movie : Entity<int>
    {
        public Movie(
            string title,
            DateTime releaseDate,
            RatingEnum rating,
            Director director)
        {
            this.Title = title;
            this.ReleaseDate = releaseDate;
            this.Rating = rating;
            this.Director = director;
        }

        public Movie()
        {
            this.Title = default!;
            this.Director= default!;
        }
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public RatingEnum Rating { get; set; }

        public Director Director { get; set; }
    }
}
