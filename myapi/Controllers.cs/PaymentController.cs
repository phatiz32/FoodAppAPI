using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myapi.Interfaces;
using Newtonsoft.Json;

namespace myapi.Controllers.cs
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoService _momoService;

        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }
        [HttpGet("momo/return")]
        public IActionResult MomoReturn()
        {
            var response = _momoService.PaymentExecuteAsync(Request.Query);
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