using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Dtos.Order;
using myapi.Interfaces;
using myapi.Models;

public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDBContext _context;
    private readonly IMomoService _momoService;

    public OrderController(
        IOrderRepository orderRepo,
        UserManager<AppUser> userManager,
        ApplicationDBContext context, IMomoService momoService)
    {
        _orderRepo = orderRepo;
        _userManager = userManager;
        _context = context;
        _momoService = momoService;
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
    public async Task<IActionResult> getListOrder()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        var orderResult =await _orderRepo.getOrderInforAsync(user.Id);
        return Ok(orderResult);
    }

   
}
