using Minimal_API.Domain.DTOs;
using Minimal_API.Domain.Interfaces;
using Minimal_API.Domain.ModelViews;
using Minimal_API.Models;

namespace Minimal_API.Domain.Services
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly minimalApiContext _contexto;

        public AdministradorServico(minimalApiContext contexto)
        {
            _contexto = contexto;
        }

        public void Incluir(Administrador administrador)
        {
            _contexto.Administrador.Add(administrador);
            _contexto.SaveChanges(); 
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            var adm = _contexto.Administrador.Where(adm => adm.Email == loginDTO.Email && adm.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }

        public List<Administrador> Todos(int? pagina)
        {
            int itensPorPagina = 10;
            List<Administrador> adms = _contexto.Administrador.ToList();

            if (pagina != null)
                adms = adms.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina).ToList();
            return adms;
        }

        public Administrador? BuscaPorId(int id)
        {
            var adm = _contexto.Administrador.FirstOrDefault(v => v.Id == id);
            return adm;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _contexto.SaveChangesAsync() > 0;
        }
    }
}
