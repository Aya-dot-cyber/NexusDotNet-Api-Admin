using Microsoft.EntityFrameworkCore.Metadata;
using Nexus.Common.Enumeration;
using Nexus.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nexus.Common.Helpers
{
    public static class ExtentionMethods
    {
        public static int GetUserId(this ClaimsPrincipal User)
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
        }
        public static string GetUserMobile(this ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(x => x.Type == "Token")?.Value;
        }
        public static string GetUserName(this ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public static int GetCompanyApprovalStatus(this ClaimsPrincipal User)
        {
            var approve = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "ApprovalStatus")?.Value);
            return approve;
        }

        public static string GetUserAvatar(this ClaimsPrincipal User)
        {
            var avatar = User.Claims.FirstOrDefault(x => x.Type == "Avatar")?.Value;
            return avatar;
        }

        public static int GetUserType(this ClaimsPrincipal User)
        {
            var x = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Type")?.Value);
            return x;
        }

        public static string GetUserEmail(this ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public static int GetLanguage(string Clanguage)
        {
            return Clanguage.Contains("ar") ? (int)ELanguages.AR : (int)ELanguages.EN;
        }
        public static void UpdateClaim(this ClaimsPrincipal User, Claim newClaim)
        {

            foreach (var identity in User.Identities)
            {
                var claimsToRemove = identity
                  .FindAll(claim => claim.Type == newClaim.Type)
                  .ToArray();
                foreach (var claim in claimsToRemove)
                {
                    var tt = identity.TryRemoveClaim(claim);
                }
                identity.AddClaim(newClaim);
            }
        }

        public static bool IsValidEmail(this string Email)
        {
            try
            {
                MailAddress m = new MailAddress(Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPhone(this string Phone)
        {
            try
            {
                // validate phone number agiainst regular expression 
                Regex regex = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
                Match match = regex.Match(Phone);
                return match.Success;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsMaxLargerMin(double? MinTargetSalary, double? MaxTargetSalary)
        {
            if (MaxTargetSalary > MinTargetSalary)
                return true;

            return false;
        }

        public static bool IsValidURL(this string URL)
        {
            try
            {
                // validate phone number agiainst regular expression 
                Regex regex = new Regex(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");
                Match match = regex.Match(URL);
                return match.Success;
            }
            catch (FormatException)
            {
                return false;
            }
        }
     

        public static bool IsValidDocExtention(string docName)
        {
            try
            {
                string[] imageEndsWith = { ".PDF", ".DOC", ".DOCX", ".PPT", ".PPTX", ".JPG", ".PNG", ".JPEG" };
                if (!imageEndsWith.Any(x => docName.ToUpper().EndsWith(x)))
                {
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidImageExtention(string docName)
        {
            try
            {
                string[] imageEndsWith = { ".SVG", ".JPG", ".PNG", ".JPEG" };
                if (!imageEndsWith.Any(x => docName.ToUpper().EndsWith(x)))
                {
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        

     

     
    }
}
