using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Domain.Entities
{
    public class Country :Common
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? NationalityAr { get; set; }
        public string? NationalityEn { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }
        public int PhoneLength { get; set; }
        public string? PhoneHint { get; set; }
        public string? CurrencyAr { get; set; }
        public string? CurrencyEn { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<User> Users { get; set; }
    }
}
