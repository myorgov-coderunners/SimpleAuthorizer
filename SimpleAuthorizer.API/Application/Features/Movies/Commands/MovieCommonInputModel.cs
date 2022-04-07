using SimpleAuthorizer.API.Domain.Models;

namespace SimpleAuthorizer.API.Application.Features.Movies.Commands
{
    public class MovieCommonInputModel
    {
        public string Title { get; set; } = default!;

        public DateTime ReleaseDate { get; set; } = default!;

        public RatingEnum Rating { get; set; } = default!;

        public int DirectorId { get; set; }
    }
}
