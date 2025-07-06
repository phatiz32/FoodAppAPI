using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Order
{
    public class CreateOrderResultDto
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}