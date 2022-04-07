using AutoMapper;
using SimpleAuthorizer.API.Domain.Models;
using SimpleAuthorizer.Common.Mappings;

namespace SimpleAuthorizer.API.Application.Features.Directors.Queries
{
    public class DirectorCommonOutputModel : IMapFrom<Director>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public IEnumerable<DirectorMoviesOutputModel> Movies { get; set; } = 
            new List<DirectorMoviesOutputModel>();

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Director, DirectorCommonOutputModel>();
    }

    public class DirectorMoviesOutputModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Movie, DirectorMoviesOutputModel>();
    }
}
