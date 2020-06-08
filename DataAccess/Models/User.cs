using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Rank { get; set; }
        public Address Address { get; set; }
        public string IsValid { get; set; }
    }
}
