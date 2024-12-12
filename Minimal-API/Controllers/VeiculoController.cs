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
    public class VeiculoController : Controller
    {
        private readonly IVeiculoServico _veiculoServico;
        private readonly IMapper _mapper;

        public VeiculoController(IVeiculoServico veiculoServico, IMapper mapper)
        {
            _mapper = mapper;
            _veiculoServico = veiculoServico;
        }

        [Authorize(Roles = "Adm,Editor")]
        [HttpPost("CadastroVeiculos")]
        public ActionResult CadastroVeiculos([FromBody] VeiculoDTO veiculoDTO)
        {
            ErrosDeValidacao validacao = _veiculoServico.ValidaDTO(veiculoDTO);

            if (validacao.Mensagens.Count > 0)
                return BadRequest(validacao);

            var veiculo = _mapper.Map<Veiculo>(veiculoDTO);
            _veiculoServico.Incluir(veiculo);

            return Ok(veiculo);
        }

        [Authorize(Roles = "Adm,Editor")]
        [HttpGet("RetornaVeiculos")]
        public ActionResult RetornaTodosVeiculos([FromQuery] int? pagina)
        {
            var veiculos = _veiculoServico.Todos(pagina);

            if (veiculos == null) return NotFound();

            return Ok(veiculos);
        }

        [Authorize(Roles = "Adm")]
        [HttpGet("{id}")]
        public ActionResult BuscaPorId([FromRoute] int id)
        {
            var veiculo = _veiculoServico.BuscaPorId(id);

            if (veiculo == null) return NotFound();

            return Ok(veiculo);
        }

        [Authorize(Roles = "Adm")]
        [HttpPut("{id}")]
        public ActionResult EditarVeiculo([FromRoute] int id, VeiculoDTO veiculoDTO)
        {
            var veiculo = _veiculoServico.BuscaPorId(id);
            if (veiculo == null) return NotFound();

            var validacao = _veiculoServico.ValidaDTO(veiculoDTO);
            if (validacao.Mensagens.Count > 0)
                return BadRequest(validacao);
            
            veiculo.Nome = veiculoDTO.Nome;
            veiculo.Marca = veiculoDTO.Marca;
            veiculo.Ano = veiculoDTO.Ano;

            _veiculoServico.Atualizar(veiculo);

            return Ok(veiculo);
        }

        [Authorize(Roles = "Adm")]
        [HttpDelete("{id}")]
        public ActionResult ExcluirVeiculo([FromRoute] int id)
        {
            var veiculo = _veiculoServico.BuscaPorId(id);
            if (veiculo == null) return NotFound();

            _veiculoServico.Apagar(veiculo);

            return NoContent();
        }
    }
}
