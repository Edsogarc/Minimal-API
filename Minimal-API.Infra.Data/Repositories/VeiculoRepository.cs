using Minimal_API.Domain.Entities;
using Minimal_API.Domain.Interfeces;
using Minimal_API.Infra.Data.Context;

namespace Minimal_API.Infra.Data.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly ApplicationDbContext _contexto;

        public VeiculoRepository(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public Veiculo? Apagar(Veiculo veiculo)
        {
            if (_contexto.Veiculo.Any(v => v.Id == veiculo.Id))
            {
                _contexto.Veiculo.Remove(veiculo);
                _contexto.SaveChanges();
                return veiculo;
            }
            return null;
        }

        public Veiculo? Atualizar(Veiculo veiculo)
        {
            if (_contexto.Veiculo.Any(v => v.Id == veiculo.Id))
            {
                _contexto.Veiculo.Update(veiculo);
                _contexto.SaveChanges();
                return veiculo;
            }
            return null;
        }

        public Veiculo? BuscaPorId(int id)
        {
            var veiculo = _contexto.Veiculo.FirstOrDefault(v => v.Id == id);
            return veiculo;
        }

        public Veiculo? Incluir(Veiculo veiculo)
        {
            if (veiculo == null)
                return null;

            _contexto.Veiculo.Add(veiculo);
            _contexto.SaveChanges();
            return veiculo;
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            int itensPorPagina = 10;
            List<Veiculo> veiculos = _contexto.Veiculo.ToList();
            if (veiculos.Count() > 0)
            {
                if (nome != null)
                    veiculos = veiculos.Where(v => v.Nome.ToLower().Contains(nome)).ToList();
                if (marca != null)
                    veiculos = veiculos.Where(v => v.Marca.ToLower().Contains(marca)).ToList();

            }
            if (pagina != null)
                veiculos = veiculos.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina).ToList();
            return veiculos;
        }

    }
}
