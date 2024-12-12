using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.Application.DTOs;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Account;

namespace Minimal_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthenticate _authenticate;

        public UsuarioController(IUsuarioService usuarioService, IAuthenticate authenticate)
        {
            _usuarioService = usuarioService;
            _authenticate = authenticate;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult LoginUsario([FromBody] Login login)
        {
            if (login == null) return BadRequest("Dados inválidos");
            var existe = _authenticate.UserExists(login.Email);
            if (!existe) return Unauthorized("Usuário não existe");

            var result = _authenticate.Authenticate(login.Email, login.Password);
            if(!result) return Unauthorized("Usuário ou senha inválida");

            var usuario = _usuarioService.BuscaPorEmail(login.Email);

            var token = _authenticate.GenerateToken(usuario.Id, usuario.Email, usuario.Perfil);
            return Ok($"Token: {token}");

        }
        
        //[Authorize(Roles = "Administrador")]
        [HttpPost("Cadastro")]
        public ActionResult CadastroUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return BadRequest("Dados inválidos");
            
            var emailExiste = _authenticate.UserExists(usuarioDTO.Email);
            if (emailExiste) return BadRequest("Esse E-mail já possui cadastro");

            var usuario = _usuarioService.Incluir(usuarioDTO);
            if (usuario == null) return BadRequest("Ocorreu erro no cadastro");

            return Created($"/administrador/{usuarioDTO.Id}", usuarioDTO);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("TodosAdministradores")]
        public ActionResult RetornaUsuarios([FromQuery] int? pagina)
        {
            var usuarioDTOs = _usuarioService.Todos(pagina);

            return Ok(usuarioDTOs);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public ActionResult BuscaPorId([FromRoute] int id)
        {
            var usuarioDTO = _usuarioService.BuscaPorId(id);

            if (usuarioDTO == null) return NotFound();

            return Ok(usuarioDTO);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public ActionResult EditarVeiculo([FromRoute] int id)
        {
            var usuarioDTO = _usuarioService.BuscaPorId(id);
            if (usuarioDTO == null) return NotFound("Veículo não encontrado");

            _usuarioService.Atualizar(usuarioDTO);

            return Ok(usuarioDTO);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public ActionResult ExcluirVeiculo([FromRoute] int id)
        {
            var usuarioDTO = _usuarioService.BuscaPorId(id);
            if (usuarioDTO == null) return NotFound();

            _usuarioService.Apagar(usuarioDTO);

            return Ok($"{usuarioDTO}");
        }

    }
}
