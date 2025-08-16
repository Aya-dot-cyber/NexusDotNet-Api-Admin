using Nexus.Application.Interfaces;
using Nexus.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Implementation
{
    public class LookupService :ILookupService
    {
        private readonly Context _context;

        public LookupService(Context context)
        {
            _context = context;
                
        }

        public string GetCountryCodeFromCache(int Id)
        {
            var data = _context.Countries.Where(x => x.Id == Id).Select(x => x.Code).FirstOrDefault();
            return data;
        }
    }
}
