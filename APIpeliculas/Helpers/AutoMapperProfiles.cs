using APIpeliculas.Entities;
using APIpeliculas.Models;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace APIpeliculas.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genero, GeneroViewModel>().ReverseMap();
            CreateMap<GeneroCreacionViewModel, Genero>();
            CreateMap<Actor, ActorViewModel>().ReverseMap();
            CreateMap<ActorCreacionModel, Actor>().ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<CinesCreacionModel, Cine>().ForMember(x => x.Ubicacion, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));
            CreateMap<Cine, CineViewModel>()
                .ForMember(x => x.Latitud, dto => dto.MapFrom(campo => campo.Ubicacion.Y))
                .ForMember(x => x.Longitud, dto => dto.MapFrom(campo => campo.Ubicacion.X));
        }
    }
}
