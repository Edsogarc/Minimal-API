using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.Domain.DTOs;
using Minimal_API.Domain.Interfaces;
using Minimal_API.Domain.ModelViews;
using Minimal_API.Models;

namespace Minimal_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : Controller
    {
        private readonly IAdministradorServico _administradorServico;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AdministradorController(ITokenService tokenService, IAdministradorServico administradorServico, IMapper mapper)
        {
            _tokenService = tokenService;
            _administradorServico = administradorServico;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult LoginAdministrador([FromBody] LoginDTO loginDTO)
        {
            var adm = _administradorServico.Login(loginDTO);

            if (adm != null)
            {
                string token = _tokenService.CreateToken(adm);
                return Ok(new AdministradorLogado
                {
                    Email = adm.Email,
                    Perfil = adm.Perfil,
                    Token = token
                });
            }
            else
                return Unauthorized("Erro login");
        }

        [Authorize(Roles = "Adm")]
        [HttpPost("Cadastro")]
        public ActionResult CadastroAdministrador([FromBody] AdministradorDTO administradorDTO)
        {
            var validacao = new ErrosDeValidacao();

            if (string.IsNullOrEmpty(administradorDTO.Email))
                validacao.Mensagens.Add("Insira o E-mail");
            if (string.IsNullOrEmpty(administradorDTO.Senha))
                validacao.Mensagens.Add("Insira a senha");
            if (administradorDTO.Perfil == null)
                validacao.Mensagens.Add("Insira o Perfil");

            if (validacao.Mensagens.Count > 0)
                return BadRequest(validacao);

            var administrador = _mapper.Map<Administrador>(administradorDTO);
            if (string.IsNullOrEmpty(administrador.Perfil)) administrador.Perfil = "Editor";
            _administradorServico.Incluir(administrador);

            var admModel = _mapper.Map<AdministradorModelView>(administrador);
            return Created($"/administrador/{admModel.Id}", admModel);
        }

        [Authorize(Roles = "Adm")]
        [HttpGet("TodosAdministradores")]
        public ActionResult RetornaAdministradores([FromQuery]int? pagina)
        {
            var adms = new List<AdministradorModelView>();
            var administradores = _administradorServico.Todos(pagina);

            var admsModel = _mapper.Map<List<AdministradorModelView>>(administradores);
            return Ok(admsModel);
        }

        [Authorize(Roles = "Adm")]
        [HttpGet("{id}")]
        public ActionResult BuscaPorId([FromRoute] int id)
        {
            var adm = _administradorServico.BuscaPorId(id);

            if (adm == null) return NotFound();

            return Ok(adm);
        }
    }
}
