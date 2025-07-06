using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public string ShippingAddress { get; set; }
    }
}