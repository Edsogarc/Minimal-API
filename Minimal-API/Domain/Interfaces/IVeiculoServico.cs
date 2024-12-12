﻿using Minimal_API.Domain.DTOs;
using Minimal_API.Domain.ModelViews;
using Minimal_API.Models;

namespace Minimal_API.Domain.Interfaces
{
    public interface IVeiculoServico
    {
        List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null);

        Veiculo? BuscaPorId(int id);
        void Incluir(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Apagar(Veiculo veiculo);
        ErrosDeValidacao ValidaDTO(VeiculoDTO veiculoDTO);
    }
}