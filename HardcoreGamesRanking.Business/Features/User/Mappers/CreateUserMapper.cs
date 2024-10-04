namespace HardcoreGamesRanking.Business.Features.User.Mappers
{
    using Core.Entities;
    using AutoMapper;
    using Business.Features.User.Dtos;
    using Core.Extensions;

    public class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<CreateUserDto, Usuario>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.EncryptString()));
        }
    }
}