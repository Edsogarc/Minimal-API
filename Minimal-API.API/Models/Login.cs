using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_API.Application.DTOs
{
    public class Login
    {
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
