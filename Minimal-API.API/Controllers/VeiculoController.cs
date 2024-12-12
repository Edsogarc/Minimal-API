using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.Application.DTOs;
using Minimal_API.Application.Interfaces;

namespace Minimal_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : Controller
    {
        private readonly IVeiculoService _veiculoService;
        public VeiculoController(IVeiculoService veiculoServico)
        {
            _veiculoService = veiculoServico;
        }

        [Authorize(Roles = "Adm,Editor")]
        [HttpPost("CadastroVeiculos")]
        public ActionResult CadastroVeiculos([FromBody] VeiculoDTO veiculoDTO)
        {
            var veiculoTemp = _veiculoService.Incluir(veiculoDTO);

            if (veiculoTemp == null)
                return BadRequest("Veículo não cadastrado");
            
            _veiculoService.Incluir(veiculoDTO);
            return Ok(veiculoDTO);
        }

        [Authorize(Roles = "Adm,Editor")]
        [HttpGet("RetornaVeiculos")]
        public ActionResult RetornaTodosVeiculos([FromQuery] int? pagina)
        {
            var veiculos = _veiculoService.Todos(pagina);

            if (veiculos == null) return NotFound();

            return Ok(veiculos);
        }

        [Authorize(Roles = "Adm")]
        [HttpGet("{id}")]
        public ActionResult BuscaPorId([FromRoute] int id)
        {
            var veiculo = _veiculoService.BuscaPorId(id);

            if (veiculo == null) return NotFound();

            return Ok(veiculo);
        }

        [Authorize(Roles = "Adm")]
        [HttpPut("{id}")]
        public ActionResult EditarVeiculo([FromRoute] int id, VeiculoDTO veiculoDTO)
        {
            var veiculo = _veiculoService.BuscaPorId(id);
            if (veiculo == null) return NotFound("Veículo não encontrado");

            _veiculoService.Atualizar(veiculo);

            return Ok(veiculo);
        }

        [Authorize(Roles = "Adm")]
        [HttpDelete("{id}")]
        public ActionResult ExcluirVeiculo([FromRoute] int id)
        {
            var veiculo = _veiculoService.BuscaPorId(id);
            if (veiculo == null) return NotFound();

            _veiculoService.Apagar(veiculo);

            return NoContent();
        }
    }
}
