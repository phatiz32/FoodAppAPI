using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Models
{
    public class Payment
    {
        [Key]
        public string MomoPaymentId { get; set; }

        public long Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public string Method { get; set; } = "Momo";
        public string? orderInfo{ get; set; }
        public Order? Order { get; set; }
    }
}