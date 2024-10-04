namespace HardcoreGamesRanking.Business.Features.VideoGame.Mappers
{
    using AutoMapper;
    using Business.Features.VideoGame.Dtos;
    using Core.Entities;

    public class GetAllVideoGameMapper : Profile
    {
        public GetAllVideoGameMapper()
        {
            CreateMap<Videojuego, GetAllVideoGameDto>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.Anio, opt => opt.MapFrom(src => src.AnioLanzamiento))
               .ForMember(dest => dest.Compania, opt => opt.MapFrom(src => src.IdCompaniaNavigation.Nombre))
               .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio));
        }
    }
}