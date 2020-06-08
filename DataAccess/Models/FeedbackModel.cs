using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class FeedbackModel
    {
        public string Name { get; set; }
        public string Feedback { get; set; }
        public long CustomerId { get; set; }
    }
}
