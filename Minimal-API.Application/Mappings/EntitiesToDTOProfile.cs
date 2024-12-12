using AutoMapper;
using Minimal_API.Application.DTOs;
using Minimal_API.Domain.Entities;

namespace Minimal_API.Application.Mappings
{
    public class EntitiesToDTOProfile : Profile
    {
        public EntitiesToDTOProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
