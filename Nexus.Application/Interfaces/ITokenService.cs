using Nexus.Common.Models;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(UserDto user);
        bool ValidateToken(string token);
        //Task<bool> UpdateFirebaseToken(FCMDataDto obj, int LangId);
        //Task<bool> DeleteFirebaseToken(FCMDataDto obj);
    }
}

