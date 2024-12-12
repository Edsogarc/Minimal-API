using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Minimal_API.Domain.ModelViews
{
    public record AdministradorModelView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; } = default!;

        [StringLength(10)]
        public string Perfil { get; set; } = default!;
    }
}
