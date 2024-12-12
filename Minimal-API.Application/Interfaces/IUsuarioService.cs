using Minimal_API.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_API.Application.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioDTO? Login(UsuarioDTO usuarioDTO);
        UsuarioDTO? Incluir(UsuarioDTO usuarioDTO);
        UsuarioDTO? Apagar(UsuarioDTO usuarioDTO);

        UsuarioDTO? Atualizar(UsuarioDTO usuarioDTO);

        List<UsuarioDTO> Todos(int? pagina);

        UsuarioDTO? BuscaPorId(int id);
        UsuarioDTO? BuscaPorEmail(string email);
    }
}
