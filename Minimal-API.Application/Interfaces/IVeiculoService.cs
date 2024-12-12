using Minimal_API.Application.DTOs;

namespace Minimal_API.Application.Interfaces
{
    public interface IVeiculoService
    {
        List<VeiculoDTO> Todos(int? pagina = 1, string? nome = null, string? marca = null);

        VeiculoDTO? BuscaPorId(int id);
        VeiculoDTO? Incluir(VeiculoDTO veiculo);
        VeiculoDTO? Atualizar(VeiculoDTO veiculo);
        VeiculoDTO? Apagar(VeiculoDTO veiculo);
    }
}
