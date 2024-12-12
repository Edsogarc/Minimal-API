using Minimal_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_API.Domain.Interfeces
{
    public interface IVeiculoRepository
    {
        List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null);
        Veiculo? BuscaPorId(int id);
        Veiculo? Incluir(Veiculo veiculo);
        Veiculo? Atualizar(Veiculo veiculo);
        Veiculo? Apagar(Veiculo veiculo);
    }
}
