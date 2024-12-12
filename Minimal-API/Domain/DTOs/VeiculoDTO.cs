using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Minimal_API.Domain.DTOs
{
    public class VeiculoDTO
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Marca { get; set; } = default!;

        public int Ano { get; set; } = default!;
    }
}
