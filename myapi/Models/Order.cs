using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}