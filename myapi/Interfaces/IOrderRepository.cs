using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.Order;

namespace myapi.Interfaces
{
    public interface IOrderRepository
    {
        Task<CreateOrderResultDto?> CreateOrderFromSelectedCartItemsAsync(string userId, string shippingAddress);
        Task<List<OrderInfoDto>> getOrderInforAsync(string userId);

    }
}