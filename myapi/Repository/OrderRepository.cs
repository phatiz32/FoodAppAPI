using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Data;
using Microsoft.EntityFrameworkCore;
using myapi.Interfaces;
using myapi.Models;
using myapi.Dtos.Order;
using myapi.Mappers;

namespace myapi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
            
        }
        public async Task<CreateOrderResultDto> CreateOrderFromSelectedCartItemsAsync(string userId, string shippingAddress)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                            .ThenInclude(ci => ci.FoodItem)
                        .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems.Any(ci => ci.IsSelected))
            {
                return null;
            }
            var selectedItems = cart.CartItems.Where(ci => ci.IsSelected == true).ToList();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var order = new Order
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                CreatedAt = DateTime.UtcNow,
                OrderItems = selectedItems.Select(ci => new OrderItem
                {
                    FoodItemId = ci.FoodItemId,
                    Quantity = ci.Quantity,
                    PriceAtOrderTime = ci.FoodItem.Price
                }).ToList(),
                PhoneNumber=user.PhoneNumber,   
                TotalAmount = selectedItems.Sum(ci => ci.Quantity * ci.FoodItem.Price)
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return new CreateOrderResultDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount
            };
        }

        public async Task<List<OrderInfoDto>> getOrderInforAsync(string userId)
        {
            return await _context.Orders.Include(o => o.User)
            .Where(o => o.UserId == userId).Select(o=>o.toOrderInforDto()).ToListAsync();
            
        }
    }
}