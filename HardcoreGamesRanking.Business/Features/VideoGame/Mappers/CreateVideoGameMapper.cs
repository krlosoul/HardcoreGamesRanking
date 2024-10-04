namespace HardcoreGamesRanking.Business.Features.VideoGame.Mappers
{
    using AutoMapper;
    using Business.Features.VideoGame.Dtos;
    using Core.Entities;

    public class CreateVideoGameMapper : Profile
    {
        public CreateVideoGameMapper()
        {
            CreateMap<CreateVideoGameDto, Videojuego>()
              .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
              .ForMember(dest => dest.IdCompania, opt => opt.MapFrom(src => src.IdCompania))
              .ForMember(dest => dest.AnioLanzamiento, opt => opt.MapFrom(src => src.AnioLanzamiento))
              .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
              .ForMember(dest => dest.Puntaje, opt => opt.MapFrom(src => src.Puntaje));
        }
    }
}