using AutoMapper;
using SimpleAuthorizer.API.Domain.Models;
using SimpleAuthorizer.Common.Mappings;

namespace SimpleAuthorizer.API.Application.Features.Movies.Queries
{
    public class MovieCommonOutputModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public DateTime ReleaseDate { get; set; }

        public RatingEnum Rating { get; set; }

        public string DirectorName { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Movie, MovieCommonOutputModel>()
                .ForMember(x => x.DirectorName, cfg => cfg.MapFrom(c => c.Director.Name));
    }
}
