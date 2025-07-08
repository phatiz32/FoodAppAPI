using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.Order;
using myapi.Models;

namespace myapi.Mappers
{
    public static class OrderMappers
    {
        public static OrderInfoDto toOrderInforDto(this Order order)
        {
            return new OrderInfoDto
            {
                Name=order.User?.FullName,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                PhoneNumber = order.PhoneNumber,
                MomoPaymentId = order.MomoPaymentId,
                PaymentStatus = order.PaymentStatus
            };
        }
    }
}