using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Template
    {
        public byte[] Photo { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public long Id { get; set; }
    }
}
