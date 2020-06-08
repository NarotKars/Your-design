using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class APIProductDetails
    {
        public long Id { get; set; }
        public decimal SellingPrice { get; set; }
        public string CompanyName { get; set; }
        public byte[] Photo { get; set; }
        public long ProductInOrderId { get; set; }
    }
}
