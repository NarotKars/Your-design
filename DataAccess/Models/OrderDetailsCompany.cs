using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class OrderDetailsCompany
    {
        public long Id { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
