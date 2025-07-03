using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.Cart;
using myapi.Models;

namespace myapi.Mappers
{
    public static class CartMappers
    {
        public static CartItem ToCartItem(this AddToCartDto dto)
        {
            return new CartItem
            {
                FoodItemId = dto.FoodItemId,
                Quantity = dto.Quantity,
            };
        }
        public static CartItemDto ToCartItemDto(this CartItem item)
        {
            return new CartItemDto
            {
                CartItemId = item.Id,
                FoodItemId = item.FoodItemId,
                Name = item.FoodItem?.Name,
                ImageUrl = item.FoodItem?.ImageUrl,
                Price = item.FoodItem?.Price ?? 0,
                Quantity = item.Quantity
            };
        }
    }
}