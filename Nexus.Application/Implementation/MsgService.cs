using Nexus.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nexus.Domain.DataContext;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Nexus.Application.Implementation
{
    public class MsgService :IMsgService
    {
        private readonly IAppSettingService _appSettingService;
        private readonly Context _context;
        public readonly IConfiguration _configuration;

        public MsgService(IConfiguration configuration, Context context,IAppSettingService appSettingService)
        {
            _context = context;
            _appSettingService = appSettingService;
            _configuration = configuration;
        }

        public async Task SendOTPEmail(string ToEmail, string code, string Name)
        {

            #region SendGridClient
            //if (_sMSConfigurationDto.SendSmsMessages)
            //{

            var client = new SendGridClient(_appSettingService.GetAppSettingValue("SendGrid", "APIKey").Result);
            var from = new EmailAddress(_appSettingService.GetAppSettingValue("SendGrid", "SenderEmail").Result);
            var subject = "Imploy OneTime Password";
            var to = new EmailAddress(ToEmail);
            var plainTextContent = "Imploy OneTime Password";
            string htmlCode = "";
            using (WebClient x = new WebClient())
            {
                //var url = "http://localhost:5023/Account/EmailTemplate?code=" + code + "&&name=" + Name;
                var url = _appSettingService.GetAppSettingValue("AppSettings", "AdminUrl").Result + "/Account/EmailTemplate?code=" + code + "&&name=" + Name;
                htmlCode = x.DownloadString(url.ToString());
            }


            var htmlContent = htmlCode;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            // }
            #endregion

           

        }
    }
}
