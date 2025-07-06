using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Cart
{
    public class SelectedCartItemDto
    {
        public List<int> CartItemIds { get; set; }
        public bool IsSelected { get; set; }
    }
}