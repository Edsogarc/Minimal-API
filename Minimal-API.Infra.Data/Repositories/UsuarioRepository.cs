using Minimal_API.Domain.Entities;
using Minimal_API.Domain.Interfaces;
using Minimal_API.Infra.Data.Context;

namespace Minimal_API.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _contexto;

        public UsuarioRepository(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public Usuario? Incluir(Usuario usuario)
        {
            if (_contexto.Usuario.Any(us => us.Email == usuario.Email))
                return null;
            _contexto.Usuario.Add(usuario);
            _contexto.SaveChanges();
            return usuario;
        }
        public Usuario? Apagar(Usuario usuario)
        {
            var usuarioExistente = _contexto.Usuario.Find(usuario.Id);

            if (usuarioExistente != null)
            {
                _contexto.Usuario.Remove(usuarioExistente);
                _contexto.SaveChanges();
                return usuarioExistente;
            }
            return null;
        }

        public Usuario? Atualizar(Usuario usuario)
        {
            var usuarioExistente = _contexto.Usuario.Find(usuario.Id);

            if (usuarioExistente != null)
            {
                _contexto.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
                _contexto.SaveChanges();
                return usuarioExistente;
            }
            return null;
        }

        public Usuario? Login(Usuario usuario)
        {
            var usuarioTemp = _contexto.Usuario.Where(us => us.Email.ToLower() == usuario.Email.ToLower()).FirstOrDefault();
            return usuarioTemp;
        }

        public List<Usuario> Todos(int? pagina)
        {
            int itensPorPagina = 10;
            List<Usuario> usuarioTemp = _contexto.Usuario.ToList();

            if (pagina != null)
                usuarioTemp = usuarioTemp.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina).ToList();
            return usuarioTemp;
        }

        public Usuario? BuscaPorId(int id)
        {
            var usuarioTemp = _contexto.Usuario.FirstOrDefault(v => v.Id == id);
            return usuarioTemp;
        }
        public Usuario? BuscaPorEmail(string email)
        {
            var usuarioTemp = _contexto.Usuario.FirstOrDefault(v => v.Email == email);
            return usuarioTemp;
        }
    }
}
