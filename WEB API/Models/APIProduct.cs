using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class APIProduct
    {
        public long Id { get; set; }
        public long ImageId { get; set; }
        public int CategoryId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        
    }
}
