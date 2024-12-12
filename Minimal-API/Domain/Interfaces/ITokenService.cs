using Minimal_API.Models;

namespace Minimal_API.Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Administrador administrador);
    }
}
