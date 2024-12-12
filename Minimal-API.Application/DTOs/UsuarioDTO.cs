using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;


namespace Minimal_API.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(255, ErrorMessage = "O nome deve ter até 255 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [MaxLength(255, ErrorMessage = "O E-mail deve ter até 255 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(100, ErrorMessage = "A senha deve conter até 100 caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo até 8 caracteres")]
        [NotMapped]
        public string Password { get; set; }
        [Required(ErrorMessage = "O Perfil é obrigatório")]
        [MaxLength(15, ErrorMessage = "O Perfil deve conter até 15 caracteres")]
        public string Perfil { get; set; }
    }
}

