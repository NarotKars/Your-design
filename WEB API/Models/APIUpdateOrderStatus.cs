using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class APIUpdateOrderStatus
    {
        public long Id { get; set; }
        public APIAddress Address { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public long CustomerId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
