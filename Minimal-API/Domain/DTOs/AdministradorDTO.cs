using System.ComponentModel.DataAnnotations;

namespace Minimal_API.Domain.DTOs
{
    public class AdministradorDTO
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Senha { get; set; } = default!;

        [StringLength(10)]
        public string Perfil { get; set; } = default!;
    }
}
