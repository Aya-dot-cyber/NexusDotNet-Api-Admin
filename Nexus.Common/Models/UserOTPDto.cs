using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Common.Models
{
    public class UserOTPDto
    {
        public int Id { get; set; }
        public int OTP { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
    }
}
