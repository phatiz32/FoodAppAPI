using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Models;

namespace myapi.Interfaces
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}