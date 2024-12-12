using Minimal_API.Domain.Validations;

namespace Minimal_API.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Perfil { get; set; }
        public Usuario(int id, string nome, string email, string perfil)
        {
            DomainExceptionValidation.When(id < 0, "O Id do administrador deve ser positivo");
            Id = id;
            ValidateDomain(nome, email, perfil);
        }

        public Usuario(string nome, string email, string perfil)
        {
            ValidateDomain(nome, email, perfil);
        }

        public void AlterarSenha(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        private void ValidateDomain(string nome, string email, string perfil)
        {
            DomainExceptionValidation.ThrowIfNullOrEmpty(nome, "O nome é obrigatório");
            DomainExceptionValidation.When(nome.Length > 255, "O nome deve ter até 255 caracteres");
            DomainExceptionValidation.ThrowIfNullOrEmpty(email, "O e-mail é obrigatório");
            DomainExceptionValidation.When(email.Length > 255, "O e-mail deve ter até 255 caracteres");
            DomainExceptionValidation.ThrowIfNullOrEmpty(perfil, "O perfil é obrigatório");
            DomainExceptionValidation.When(
                perfil != "Administrador" && perfil != "Editor",
                "O perfil deve ser 'Administrador' ou 'Editor'"
            );
            Nome = nome;
            Email = email;
            Perfil = perfil;
        }
    }
}
