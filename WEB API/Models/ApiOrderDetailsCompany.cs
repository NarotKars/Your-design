using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class ApiOrderDetailsCompany
    {
        public long Id { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
