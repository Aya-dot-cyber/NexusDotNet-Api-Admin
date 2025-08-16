using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Interfaces;
using Nexus.Common.Models;
using Nexus.Domain.DataContext;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Common.Helpers;

namespace Nexus.Application.Implementation
{
    public class UserOTPService : IUserOTPService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IMsgService _messageService;
        private readonly IAppSettingService _appSettingService;

        public UserOTPService(IMsgService messageService,
            IAppSettingService appSettingService
            , Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _messageService = messageService;
            _appSettingService = appSettingService;
        }

        public async Task<UserOTPDto> GetByUserId(int UserId)
        {
            var data = await _context.UserOTPs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.UserId == UserId);
            return _mapper.Map<UserOTPDto>(data);
        }

        public async Task<UserOTPDto> GetByKey(string Key)
        {
            var data = await _context.UserOTPs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Key == Key);
            return _mapper.Map<UserOTPDto>(data);
        }

        public async Task<UserOTPDto> Insert(int UserId)
        {
            // expire previce otp for customer 
            var lastOTP = await _context.UserOTPs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.UserId == UserId);
            if (lastOTP != null)
                lastOTP.IsDeleted = true;

            UserOTP userOTP = new UserOTP
            {
                UserId = UserId,
                OTP = Helper.GenerateOTP(),
                //OTP=1234,
                Key = Helper.GenerateOtpKey(10).ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            await _context.UserOTPs.AddAsync(userOTP);
            return await _context.SaveChangesAsync() > 0 ? _mapper.Map<UserOTPDto>(userOTP) : null;
        }
        public async Task<bool> ExpireOTP(int id)
        {
            var lastOTP = await _context.UserOTPs.OrderByDescending(a => a.Id).FirstOrDefaultAsync(a => a.Id == id);
            if (lastOTP != null)
                lastOTP.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> SendOTP(string? Email, string? phone, string OTP, string Name)
        {
            try
            {
                var IsKsaSMS = await _appSettingService.GetAppSettingValue("SMSConfig", "IsKsaSMS");
                string message = "Recruting OneTime Password :" + OTP + "  أدخل هذا الكود بنفسك ولا تفصح عنه لأي شخص";
                if (!string.IsNullOrEmpty(Email))
                    await _messageService.SendOTPEmail(Email, OTP, Name/*new MailRequestDto { ToEmail = Email, Subject = "Recruting Verification Code", Body = message }*/);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<string?> ReSendOTP(string Key)
        {
            try
            {
                var UserOTP = await _context.UserOTPs.IgnoreQueryFilters().OrderByDescending(x => x.Id).Where(x => x.Key == Key).Include(x => x.User).FirstOrDefaultAsync();
                if (UserOTP != null)
                {
                    var createUserOTP = await Insert(UserOTP.UserId);
                    if (createUserOTP != null)
                    {
                        var send = SendOTP(UserOTP.User.Email, null, createUserOTP.OTP.ToString(), UserOTP.User.FullName);
                        return createUserOTP.Key;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}


