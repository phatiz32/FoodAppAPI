using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Dtos.Cart;
using myapi.Interfaces;
using myapi.Mappers;
using myapi.Models;

namespace myapi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext _context;
        public CartRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Cart> AddToCardAsync(string userId, AddToCartDto dto)
        {
            if (dto.Quantity <= 0)
            throw new ArgumentException("Số lượng phải lớn hơn 0");
            var cart = await _context.Carts.Include(m => m.CartItems)
            .FirstOrDefaultAsync(m => m.UserId == userId);
            //neu chua co gio hang thi tao moi
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()

                };
                await _context.Carts.AddAsync(cart);

            }
            var existingItem = cart.CartItems
            .FirstOrDefault(m => m.FoodItemId == dto.FoodItemId);
            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity + dto.Quantity;
                existingItem.Quantity = newQuantity > 20 ? 20 : newQuantity;
            }
            else
            {
                var cartItem = dto.ToCartItem();
                cartItem.Quantity = cartItem.Quantity > 20 ? 20 : cartItem.Quantity;
                cart.CartItems.Add(cartItem);
            }
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                return false;

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string userId, int cartItemId)
        {
            var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                return false;
            }
            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<CartItemDto>> GetCartItemsAsync(string userId)
        {
            var cart = await _context.Carts
            .Include(m => m.CartItems)
            .ThenInclude(mi => mi.FoodItem).FirstOrDefaultAsync(m => m.UserId == userId);
            if (cart == null || cart.CartItems == null)
            {
                return new List<CartItemDto>();

            }
            return cart.CartItems.Select(ci => ci.ToCartItemDto()).ToList();
        }

        public async Task<CartTotalDto> GetCartTotalAsync(string userId)
        {
             var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.FoodItem)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                return new CartTotalDto { TotalItems = 0, TotalPrice = 0 };

            var totalItems = cart.CartItems.Sum(i => i.Quantity);
            var totalPrice = cart.CartItems.Sum(i => i.Quantity * i.FoodItem.Price);

            return new CartTotalDto
            {
                TotalItems = totalItems,
                TotalPrice = totalPrice
            };
        }

        public async Task<bool> SetSelectedCartItemsAsync(string userId, List<int> cartItemIds, bool isSelected)
        {
            var cart = await _context.Carts
                                    .Include(c => c.CartItems)
                                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                return false;

            var itemsToUpdate = cart.CartItems
                .Where(ci => cartItemIds.Contains(ci.Id))
                .ToList();

            if (!itemsToUpdate.Any()) return false;

            foreach (var item in itemsToUpdate)
            {
                item.IsSelected = isSelected;
            }

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateCartItemQuantityAsync(string userId, UpdateCartItemDto dto)
        {
            var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return false;

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == dto.CartItemId);
            if (item == null) return false;

            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}