using System.ComponentModel.DataAnnotations;

namespace Minimal_API.Domain.ModelViews
{
    public class AdministradorLogado
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = default!;
        [StringLength(10)]
        public string Perfil { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
