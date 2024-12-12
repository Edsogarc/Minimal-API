using AutoMapper;
using Minimal_API.Domain.DTOs;
using Minimal_API.Domain.ModelViews;
using Minimal_API.Models;

namespace Minimal_API.Domain.Mappings
{
    public class EntitiesToDTOProfile : Profile
    {
        public EntitiesToDTOProfile()
        {
            CreateMap<Administrador, AdministradorModelView>().ReverseMap();
            CreateMap<Administrador, AdministradorDTO>().ReverseMap();
            CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
        }
    }
}
