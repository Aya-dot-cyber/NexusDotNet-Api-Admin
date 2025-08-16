using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Interfaces
{
    public interface IMsgService
    {
        Task SendOTPEmail(string ToEmail, string code, string Name);

    }
}
