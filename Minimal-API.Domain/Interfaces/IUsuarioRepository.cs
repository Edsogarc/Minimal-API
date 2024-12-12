using Minimal_API.Domain.Entities;

namespace Minimal_API.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario? Login(Usuario usuario);
        Usuario? Incluir(Usuario usuario);
        Usuario? Apagar(Usuario usuario);
        Usuario? Atualizar(Usuario usuario);
        List<Usuario> Todos(int? pagina);

        Usuario? BuscaPorId(int id);

        Usuario? BuscaPorEmail(string email);
    }
}
