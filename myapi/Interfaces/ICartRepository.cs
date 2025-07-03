using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.Cart;
using myapi.Models;

namespace myapi.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> AddToCardAsync(string userId, AddToCartDto dto);
        //id trong usermanger no la string
        Task<List<CartItemDto>> GetCartItemsAsync(string userId);
        Task<bool> DeleteAsync(string userId, int cartItemId);
        Task<bool> UpdateCartItemQuantityAsync(string userId, UpdateCartItemDto dto);
        Task<CartTotalDto> GetCartTotalAsync(string userId);
        Task<bool> ClearCartAsync(string userId);


    }
}