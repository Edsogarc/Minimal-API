using AutoMapper;
using Minimal_API.Application.DTOs;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Entities;
using Minimal_API.Domain.Interfeces;

namespace Minimal_API.Application.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _repository;
        private readonly IMapper _mapper;

        public VeiculoService(IVeiculoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public VeiculoDTO? Apagar(VeiculoDTO veiculoDTO)
        {
            var veiculoM = _mapper.Map<Veiculo>(veiculoDTO);
            var veiculoTemp = _repository.Apagar(veiculoM);
            return _mapper.Map<VeiculoDTO>(veiculoTemp);
        }

        public VeiculoDTO? Atualizar(VeiculoDTO veiculoDTO)
        {
            var veiculoM = _mapper.Map<Veiculo>(veiculoDTO);
            var veiculoTemp = _repository.Atualizar(veiculoM);
            return _mapper.Map<VeiculoDTO>(veiculoTemp);
        }

        public VeiculoDTO? BuscaPorId(int id)
        {
            var veiculo = _repository.BuscaPorId(id);
            return _mapper.Map<VeiculoDTO>(veiculo);
        }

        public VeiculoDTO? Incluir(VeiculoDTO veiculoDTO)
        {
            var veiculoM = _mapper.Map<Veiculo>(veiculoDTO);
            var veiculoTemp = _repository.Incluir(veiculoM);
            return _mapper.Map<VeiculoDTO>(veiculoTemp);
        }

        public List<VeiculoDTO> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            var veiculosR = _repository.Todos(pagina, nome, marca);
            var veiculosM = _mapper.Map<List<VeiculoDTO>>(veiculosR);
            return veiculosM;
        }
    }
}
