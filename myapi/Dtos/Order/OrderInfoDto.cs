using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Order
{
    public class OrderInfoDto
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string? MomoPaymentId { get; set; }
    }
}