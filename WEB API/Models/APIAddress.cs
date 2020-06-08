using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class APIAddress
    {
        [MaxLength(20)]
        public string City { get; set; }
        [MaxLength(30)]
        public string Street { get; set; }
        [MaxLength(10)]
        public string Number { get; set; }
    }
}
