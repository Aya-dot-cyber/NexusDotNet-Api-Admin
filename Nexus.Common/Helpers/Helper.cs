
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Nexus.Common.Enumeration;
using Nexus.Domain.DataContext;
using Nexus.Common.Resources;


namespace Nexus.Common.Helpers
{
    public static class Helper
    {
        public static int GenerateOTP(int Size = 4)
        {
            // Generates a random number within a range.      
            string min = "1";
            string max = "9";
            for (int i = 1; i < Size; i++)
            {
                min = min + "0";
                max = max + "9";
            }
            Random _random = new Random();
            return _random.Next(Convert.ToInt32(min), Convert.ToInt32(max));
        }


        public static string GenerateOtpKey(int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            char[] otp = new char[length];

            for (int i = 0; i < length; i++)
            {
                otp[i] = chars[random.Next(chars.Length)];
            }

            return new string(otp);
        }


    }
}
