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
        public IActionResult CadastroVeiculos([FromBody] VeiculoDTO veiculoDTO)
        {
            var veiculoTemp = _veiculoService.Incluir(veiculoDTO);

            if (veiculoTemp == null)
                return BadRequest("Veículo não cadastrado");

            return Ok(veiculoDTO);
        }

        [Authorize(Roles = "Adm,Editor")]
        [HttpGet("RetornaVeiculos")]
        public IActionResult RetornaTodosVeiculos([FromQuery] int? pagina)
        {
            var veiculosDto = _veiculoService.Todos(pagina);

            if (veiculosDto == null) return NotFound();

            return Ok(veiculosDto);
        }

        [Authorize(Roles = "Adm")]
        [HttpGet("{id}")]
        public IActionResult BuscaPorId([FromRoute] int id)
        {
            var veiculoDto = _veiculoService.BuscaPorId(id);

            if (veiculoDto == null) return NotFound();

            return Ok(veiculoDto);
        }

        [Authorize(Roles = "Adm")]
        [HttpPut("{id}")]
        public IActionResult EditarVeiculo([FromRoute] int id, [FromBody] VeiculoDTO veiculoDTO)
        {
            var veiculoDto = _veiculoService.BuscaPorId(id);
            if (veiculoDto == null) return NotFound("Veículo não encontrado");

            _veiculoService.Atualizar(veiculoDto);

            return Ok(veiculoDto);
        }

        [Authorize(Roles = "Adm")]
        [HttpDelete("{id}")]
        public IActionResult ExcluirVeiculo([FromRoute] int id)
        {
            var veiculoDto = _veiculoService.BuscaPorId(id);
            if (veiculoDto == null) return NotFound();

            _veiculoService.Apagar(veiculoDto);

            return NoContent();
        }
    }
}
