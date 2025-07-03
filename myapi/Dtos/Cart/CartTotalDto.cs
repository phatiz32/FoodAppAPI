using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Cart
{
    public class CartTotalDto
    {
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}