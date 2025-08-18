using Nexus.Common.Enumeration;
using Nexus.Common.Models;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Common.Response;

namespace Nexus.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Register(RegisterDto input);
        Task<bool> forgetpassword(string email);
        Task<TokenDto> Login(LoginDto input, string sessionId, int langId = (int)ELanguages.EN);
        Task<ResponseModel> ValidateOTP(OTPDto input, int langId);

    }
}
