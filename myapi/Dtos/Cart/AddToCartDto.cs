using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Cart
{
    public class AddToCartDto
    {
        public int FoodItemId { get; set; }
        
        public int Quantity { get; set; }
    }
}