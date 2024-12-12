using Minimal_API.Domain.Validations;

namespace Minimal_API.Domain.Entities
{
    public class Veiculo
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }

        public Veiculo(int id, string nome, string marca, int ano)
        {
            DomainExceptionValidation.When(id < 0, "O Id do Veículo deve ser positivo");
            Id = id;
            Validate(nome, marca, ano);
        }

        public Veiculo(string nome, string marca, int ano)
        {
            Id = 0;
            Validate(nome, marca, ano);
        }

        public void Update(string nome, string marca, int ano)
        {
            Validate(nome, marca, ano);
        }

        public void Validate(string nome, string marca, int ano)
        {
            DomainExceptionValidation.ThrowIfNullOrEmpty(nome, "Insira o nome do Veículo");
            DomainExceptionValidation.When(nome.Length > 150, "O Nome deve ter até 150 caracteres");

            DomainExceptionValidation.ThrowIfNullOrEmpty(marca, "Insira a Marca do Veículo");
            DomainExceptionValidation.When(marca.Length > 100, "A Marca deve ter até 100 caracteres");

            DomainExceptionValidation.When(ano < 1886 || ano > DateTime.Now.Year, "O Ano é inválido");
            Nome = nome;
            Marca = marca;
            Ano = ano;
        }
    }
}
