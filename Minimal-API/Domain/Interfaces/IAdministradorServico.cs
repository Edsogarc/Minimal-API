using Minimal_API.Domain.DTOs;
using Minimal_API.Domain.ModelViews;
using Minimal_API.Models;

namespace Minimal_API.Domain.Interfaces
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);
        void Incluir(Administrador administrador);

        List<Administrador> Todos(int? pagina);

        Administrador? BuscaPorId(int id);

        Task<bool> SaveAllAsync();
    }
}
