using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myapi.Data;
using myapi.Interfaces;
using myapi.Models;
using Newtonsoft.Json;

namespace myapi.Controllers.cs
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoService _momoService;
        private readonly ApplicationDBContext _context;

        public PaymentController(IMomoService momoService, ApplicationDBContext context)
        {
            _momoService = momoService;
            _context = context;
        }
        private int ExtractOrderId(string orderInfo)
        {
            if (string.IsNullOrEmpty(orderInfo)) return 0;

            var match = System.Text.RegularExpressions.Regex.Match(orderInfo, @"#(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int orderId))
            {
                return orderId;
            }
            return 0; // hoặc throw lỗi nếu muốn bắt buộc phải có ID
        }

        [HttpGet("momo/return")]
        public IActionResult MomoReturn()
        {
            var response = _momoService.PaymentExecuteAsync(Request.Query);
            var momoPaymentId = response.OrderId;
            var orderId = ExtractOrderId(response.OrderInfo);
            var payment = new Payment
            {
                MomoPaymentId = momoPaymentId,
                Amount = long.Parse(response.Amount),
                PaidAt = DateTime.UtcNow,
                orderInfo = response.OrderInfo,
            };
            _context.Payment.Add(payment);
            var order = _context.Orders.FirstOrDefault(o => o.Id==orderId);
            if (order != null)
            {
                order.PaymentStatus = "Đã thanh toán";
                order.MomoPaymentId = momoPaymentId;

            }
            _context.SaveChanges();
            return Ok(new
            {
                message = "Thanh toán thành công",
                response.OrderId,
                response.Amount,
                response.OrderInfo
            });
        }
        [HttpPost("momo/notify")]
        public IActionResult MomoNotify([FromForm] IFormCollection form)
        {
            var queryDictionary = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();
            foreach (var item in form)
            {
                queryDictionary[item.Key] = item.Value;
            }

            var queryCollection = new Microsoft.AspNetCore.Http.QueryCollection(queryDictionary);

            var response = _momoService.PaymentExecuteAsync(queryCollection);

            Console.WriteLine("===> MoMo Notify Called:");
            Console.WriteLine(JsonConvert.SerializeObject(response));

            return Ok(new { message = "Notify received", response.OrderId });
        }
    }
    
}