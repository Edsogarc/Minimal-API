using AutoMapper;
using Minimal_API.Application.DTOs;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Entities;
using Minimal_API.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Minimal_API.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public UsuarioDTO? Incluir(UsuarioDTO usuarioDTO)
        {
            using (var hmac = new HMACSHA256())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(usuarioDTO.Password));

                // Criar usuário com salt e hash
                var usuario = new Usuario(
                    usuarioDTO.Nome,
                    usuarioDTO.Email,
                    usuarioDTO.Perfil
                );

                // Definir senha
                usuario.AlterarSenha(passwordHash, passwordSalt);

                // Salvar usuário
                var usuarioTemp = _usuarioRepository.Incluir(usuario);
                return usuarioTemp == null ? null : _mapper.Map<UsuarioDTO>(usuarioTemp);
            }
        }

        public UsuarioDTO? Apagar(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioTemp = _usuarioRepository.Apagar(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioTemp);
        }

        public UsuarioDTO? Atualizar(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioTemp = _usuarioRepository.Atualizar(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioTemp);
        }

        public UsuarioDTO? Login(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioTemp = _usuarioRepository.Login(usuario);
            return _mapper.Map<UsuarioDTO>(usuarioDTO);
        }

        public List<UsuarioDTO> Todos(int? pagina)
        {
            var usuarios = _usuarioRepository.Todos(pagina);
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        public UsuarioDTO? BuscaPorId(int id)
        {
            var usuario = _usuarioRepository.BuscaPorId(id);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return usuarioDTO;
        }
        public UsuarioDTO? BuscaPorEmail(string email)
        {
            var usuario = _usuarioRepository.BuscaPorEmail(email);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return usuarioDTO;
        }
    }
}
