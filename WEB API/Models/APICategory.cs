using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class APICategory
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
