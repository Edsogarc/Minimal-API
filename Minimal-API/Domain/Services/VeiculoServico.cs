using Microsoft.EntityFrameworkCore;
using Minimal_API.Domain.DTOs;
using Minimal_API.Domain.Interfaces;
using Minimal_API.Domain.ModelViews;
using Minimal_API.Models;

namespace Minimal_API.Domain.Services
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly minimalApiContext _contexto;

        public VeiculoServico(minimalApiContext contexto)
        {
            _contexto = contexto;
        }

        public void Apagar(Veiculo veiculo)
        {
            if (_contexto.Veiculo.Any(v => v.Id == veiculo.Id))
            {
                _contexto.Veiculo.Remove(veiculo);
                _contexto.SaveChanges();
            }
        }

        public void Atualizar(Veiculo veiculo)
        {
            if (_contexto.Veiculo.Any(v => v.Id == veiculo.Id))
            {
                _contexto.Veiculo.Update(veiculo);
                _contexto.SaveChanges();
            }
        }

        public Veiculo? BuscaPorId(int id)
        {
            var veiculo = _contexto.Veiculo.FirstOrDefault(v => v.Id == id);
            return veiculo;
        }

        public void Incluir(Veiculo veiculo)
        {
            if (!_contexto.Veiculo.Any(v => v.Id == veiculo.Id))
            {
                _contexto.Veiculo.Add(veiculo);
                _contexto.SaveChanges();
            }
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

        public ErrosDeValidacao ValidaDTO(VeiculoDTO veiculoDTO)
        {
            var validacao = new ErrosDeValidacao();

            if (string.IsNullOrEmpty(veiculoDTO.Nome))
                validacao.Mensagens.Add("Insira o nome do Veículo");
            if (string.IsNullOrEmpty(veiculoDTO.Marca))
                validacao.Mensagens.Add("Insira a Marca do Veículo");
            if (veiculoDTO.Ano < 1950)
                validacao.Mensagens.Add("O ano do Veículo precisa ser superior a 1950");
            return validacao;
        }
    }
}
