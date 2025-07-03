using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.Cart
{
    public class UpdateCartItemDto
    {
        [Required]
         public int CartItemId { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Số lượng phải từ 1 đến 20")]
        public int Quantity { get; set; }
    }
}