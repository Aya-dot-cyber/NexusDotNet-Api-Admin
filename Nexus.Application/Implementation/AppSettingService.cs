using Nexus.Application.Interfaces;
using Nexus.Common.Enumeration;
using Nexus.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Implementation
{
    public class AppSettingService :IAppSettingService
    {
        private readonly Context _context;

        public AppSettingService(Context context)
        {
            
            _context = context;
        }
        public async Task<string> GetAppSettingValue(string Group, String Key, int langId = (int)ELanguages.EN)
        {
            var res = _context.AppSettings
                .Where(x => x.Group == Group && x.Key == Key)
                .Select(x => (langId == (int)ELanguages.AR) ? x.ValueAr : x.ValueEn).FirstOrDefault();
            return res;

        }
    }
}
