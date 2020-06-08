using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class OrderDetail
    {
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public string Status { get; set; }
        public long Id { get; set; }
    }
}
