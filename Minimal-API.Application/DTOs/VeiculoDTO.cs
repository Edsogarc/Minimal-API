using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Minimal_API.Application.DTOs
{
    public class VeiculoDTO
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o nome do Veículo")]
        [StringLength(150, ErrorMessage = "O Nome deve ter até 150 caracteres")]
        public string Nome { get; set; } = default!;

        [Required(ErrorMessage = "Insira a Marca do Veículo")]
        [StringLength(100, ErrorMessage = "A Marca deve ter até 100 caracteres")]
        public string Marca { get; set; } = default!;
        public int Ano { get; set; } = default!;
    }
}
