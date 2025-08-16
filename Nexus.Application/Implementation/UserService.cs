using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nexus.Application.Interfaces;
using Nexus.Common.Enumeration;
using Nexus.Common.Helpers;
using Nexus.Common.Models;
using Nexus.Domain.DataContext;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watan.Recruting.Common.Response;

namespace Nexus.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        private readonly IMapper? _mapper;
        private readonly IUserOTPService? _userOTPService;
        private readonly ITokenService? _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILookupService? _lookupService;
        //private readonly IAppSettingService _appSettingService;
        private string? AdminUrl;
        private ResponseModel? _responseModel;
        private readonly int? USERID;
        
        public UserService(Context context, IConfiguration config, IMapper mapper,
          IUserOTPService userOTPService, ITokenService tokenService, ILookupService lookupService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
            _userOTPService = userOTPService;
            _tokenService = tokenService;
            //_appSettingService = appSettingService;
            //_httpContextAccessor = httpContextAccessor.CheckIsNullOrContextNull();
            USERID = _httpContextAccessor.HttpContext.User.GetUserId();
            _responseModel = new ResponseModel();
            _lookupService = lookupService;


        }
        public Task<bool> forgetpassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<TokenDto> Login(LoginDto input, string sessionId, int langId = 1)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegisterDto input)
        {
           
                string Key = null;
                var check = await _context?.Users.FirstOrDefaultAsync(x =>  x.IsActive && (x.Email == input.Email || x.Phone ==input.PhoneNumber));
                if (check == null || (check.Email == input.Email && check.EmailVerified == false))
                {
                    User user = new User
                    {
                        FullName = input.FullName,
                        Email = input.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                        Phone = input.PhoneNumber,
                        Type =input.Type,
                        EmailVerified = false,
                        CountryId = input.CountryId,
                      
                    };
                    await _context.Users.AddAsync(user);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        var data = await _userOTPService.Insert(user.Id);
                        if (data != null)
                        {
                            // send OTP via service
                            var Code = _lookupService.GetCountryCodeFromCache(input.CountryId);
                            var PhoneNumber = (string.Concat(Code.TrimStart('+'), input.PhoneNumber.TrimStart('0')));
                            await _userOTPService.SendOTP(input.Email, PhoneNumber, data.OTP.ToString(), user.FullName);
                            Key = data.Key;

                        }
                    }

                }

                return Key;
            
        }

        public Task<ResponseModel> ValidateOTP(OTPDto input, int langId)
        {
            throw new NotImplementedException();
        }
    }
}
