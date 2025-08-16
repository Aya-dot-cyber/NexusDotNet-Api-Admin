using Nexus.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Interfaces
{
    public interface IAppSettingService
    {
        Task<string> GetAppSettingValue(string Group, String Key, int langId = (int)ELanguages.EN);

    }
}
