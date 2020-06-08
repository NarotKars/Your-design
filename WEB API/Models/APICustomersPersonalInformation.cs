using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class APICustomersPersonalInformation
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public APIAddress Address { get; set; }
        public string Password { get; set; }
        public string Rank { get; set; }
    }
}
