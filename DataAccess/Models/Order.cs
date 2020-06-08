using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Order
    {
        public long Id { get; set; }
        public Address Address { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public long CustomerId { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class OrderToShow
    {
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
    }
}
