using SimpleAuthorizer.Common.Models;

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

        private Movie()
        {
            this.Title = default!;
            this.Director= default!;
        }
        public string Title { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public RatingEnum Rating { get; private set; }

        public Director Director { get; private set; }

        public Movie UpdateTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public Movie UpdateReleaseDate(DateTime date)
        {
            this.ReleaseDate = date;
            return this; 
        }

        public Movie UpdateRating(RatingEnum rating)
        {
            this.Rating = rating;
            return this;
        }

        public Movie UpdateDirector(Director director)
        {
            this.Director = director;
            return this;
        }

    }
}
