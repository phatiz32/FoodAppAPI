using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Dtos.Order;
using myapi.Interfaces;
using myapi.Models;
using myapi.Models.vnpay;
using myapi.Service.vnpay;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDBContext _context;
    private readonly IMomoService _momoService;
    private readonly IVnPayService _vnPayService;

    public OrderController(
        IOrderRepository orderRepo,
        UserManager<AppUser> userManager,
        ApplicationDBContext context, IMomoService momoService, IVnPayService vnPayService)
    {
        _orderRepo = orderRepo;
        _userManager = userManager;
        _context = context;
        _momoService = momoService;
        _vnPayService = vnPayService;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var orderResult = await _orderRepo.CreateOrderFromSelectedCartItemsAsync(user.Id, dto.ShippingAddress);
        if (orderResult == null)
            return BadRequest(new { error = "Không có món nào được chọn để đặt hàng" });

        return Ok(new
        {
            message = "Đơn hàng đã được tạo thành công",
            orderResult.OrderId,
            orderResult.TotalAmount
        });
    }
    [HttpPost("create-momo")]
    [Authorize]
    public async Task<IActionResult> CreateOrderWithMomoAsync([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var orderResult = await _orderRepo.CreateOrderFromSelectedCartItemsAsync(user.Id, dto.ShippingAddress);
        if (orderResult == null)
            return BadRequest(new { error = "Không có món nào được chọn để đặt hàng" });

        var momoRequest = new OrderInfoModel
        {
            FullName = user.UserName,
            Amount = orderResult.TotalAmount,
            OrderInfo = $"Thanh toán đơn hàng #{orderResult.OrderId}"
        };

        var momoResponse = await _momoService.CreatePaymentAsync(momoRequest);

        return Ok(new
        {
            message = "Đơn hàng đã tạo thành công",
            paymentUrl = momoResponse.PayUrl,
            orderId = orderResult.OrderId
        });
    }
    [HttpGet("order")]
    [Authorize]
    public async Task<IActionResult> getListOrder(int pageSize = 5, int pageNumber = 1)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        var orderResult = await _orderRepo.getOrderInforAsync(user.Id, pageSize, pageNumber);
        return Ok(orderResult);
    }
    [HttpPost("create-vnpay")]
    [Authorize]
        public async Task<IActionResult> CreateOrderWithVnPayAsync([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // B1: Tạo đơn hàng từ giỏ hàng đã chọn
            var orderResult = await _orderRepo.CreateOrderFromSelectedCartItemsAsync(user.Id, dto.ShippingAddress);
            if (orderResult == null)
                return BadRequest(new { error = "Không có món nào được chọn để đặt hàng" });

            // B2: Tạo model gửi qua VnPay
            var vnPayRequest = new PaymentInformationModel
            {
                Amount =(int)orderResult.TotalAmount,
                OrderType = "billpayment", // hoặc type khác theo yêu cầu VNPay
                OrderDescription = $"ORDERID:{orderResult.OrderId};EMAIL:{user.Email}",
                Name = user.UserName
            };

            // B3: Gọi VnPayService để tạo URL thanh toán
            var paymentUrl = _vnPayService.CreatePaymentUrl(vnPayRequest, HttpContext);

            // B4: Trả về client để chuyển hướng sang VNPay
            return Ok(new
            {
                message = "Đơn hàng đã tạo thành công",
                paymentUrl = paymentUrl,
                orderId = orderResult.OrderId
            });
        }
}
