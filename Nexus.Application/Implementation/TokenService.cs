using Nexus.Application.Interfaces;
using Nexus.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Implementation
{
    public class TokenService : ITokenService
    {
        public string BuildToken(UserDto user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
