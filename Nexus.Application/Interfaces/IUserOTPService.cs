using Nexus.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Interfaces
{
    public interface IUserOTPService
    {
        Task<UserOTPDto> Insert(int UserId);
        Task<UserOTPDto> GetByUserId(int UsersId);
        Task<bool> SendOTP(string Email, string phone, string OTP, string Name);
        Task<UserOTPDto> GetByKey(string CustomerKey);
        Task<string?> ReSendOTP(string Key);
        Task<bool> ExpireOTP(int id);

    }
}
