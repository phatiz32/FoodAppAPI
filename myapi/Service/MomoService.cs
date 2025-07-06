using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using myapi.Interfaces;
using myapi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace myapi.Service
{
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;

        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;
        }
        
            public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model)
            {
                model.OrderId = DateTime.UtcNow.Ticks.ToString();
                model.OrderInfo = "Khách hàng: " + model.FullName + ". Nội dung: " + model.OrderInfo;
                var rawData =
                $"partnerCode={_options.Value.PartnerCode}" +
                $"&accessKey={_options.Value.AccessKey}" +
                $"&requestId={model.OrderId}" +
                $"&amount={Convert.ToInt64(model.Amount)}" +
                $"&orderId={model.OrderId}" +
                $"&orderInfo={model.OrderInfo}" +
                $"&returnUrl={_options.Value.ReturnUrl}" +
                $"&notifyUrl={_options.Value.NotifyUrl}" +
                $"&extraData=";
                var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
                var client = new RestClient(_options.Value.MomoApiUrl);
                var request = new RestRequest() { Method = Method.Post };
                request.AddHeader("Content-Type", "application/json; charset=UTF-8");
                var requestData = new
                {
                    accessKey = _options.Value.AccessKey,
                    partnerCode = _options.Value.PartnerCode,
                    requestType = _options.Value.RequestType,
                    notifyUrl = _options.Value.NotifyUrl,
                    returnUrl = _options.Value.ReturnUrl,
                    orderId = model.OrderId,
                    amount = Convert.ToInt64(model.Amount).ToString(),
                    orderInfo = model.OrderInfo,
                    requestId = model.OrderId,
                    extraData = "",
                    signature = signature
                };
                request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

                var response = await client.ExecuteAsync(request);
                Console.WriteLine("Momo raw response: " + response.Content);
                var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
                Console.WriteLine(">>> Response From Momo:");
                Console.WriteLine(response.Content);    
                return momoResponse;

            }
            private string ComputeHmacSha256(string message, string secretKey)
            {
                var keyBytes = Encoding.UTF8.GetBytes(secretKey);
                var messageBytes = Encoding.UTF8.GetBytes(message);

                using var hmac = new HMACSHA256(keyBytes);
                var hashBytes = hmac.ComputeHash(messageBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
            {
                var amount = collection.First(s => s.Key == "amount").Value;
                var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
                var orderId = collection.First(s => s.Key == "orderId").Value;
                return new MomoExecuteResponseModel()
                {
                    Amount = amount,
                    OrderId = orderId,
                    OrderInfo = orderInfo
                };
            }

    }
}