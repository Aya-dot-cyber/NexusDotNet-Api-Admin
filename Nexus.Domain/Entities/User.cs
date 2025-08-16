using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Domain.Entities
{
    public class User :Common
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? Phone { get; set; }
        public string? Password { get; set; }
        public int Type { get; set; }

        public int CountryId { get; set; }

        public bool IsActive { get; set; } = true;

        public bool? EmailVerified { get; set; }


    }
}
