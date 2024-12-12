using Minimal_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_API.Domain.Account
{
    public interface IAuthenticate
    {
        bool Authenticate(string email, string password);

        bool UserExists(string email);

        string GenerateToken(int id, string email, string perfil);
    }
}
