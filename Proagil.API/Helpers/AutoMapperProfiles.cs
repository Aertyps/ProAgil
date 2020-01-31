using System.Linq;
using AutoMapper;
using Proagil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace Proagil.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto >()
            .ForMember(dest => dest.Palestrantes, opt =>{
                opt.MapFrom(src => src.PalestranteEventos.Select(
                    x => x.Palestrante
                ).ToList());
            }).ReverseMap();

            CreateMap<Palestrante, PalestranteDto >()
            .ForMember(dest => dest.Eventos, opt =>{
                opt.MapFrom(src => src.PalestranteEventos.Select(
                    x => x.Evento
                ).ToList());
            }).ReverseMap();
            CreateMap<Lote, LoteDto >().ReverseMap();
            CreateMap<RedeSocial, RedesSociaisDto >().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

        }
    }
}