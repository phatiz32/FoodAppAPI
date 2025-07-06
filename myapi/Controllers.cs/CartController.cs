using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myapi.Dtos.Cart;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Controllers.cs
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly ICartRepository _cartRepository;
        public CartController(UserManager<AppUser> userManager, ICartRepository cartRepository)
        {
            _usermanager = userManager;
            _cartRepository = cartRepository;
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddToCard(AddToCartDto dto)
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                await _cartRepository.AddToCardAsync(user.Id, dto);
                return Ok(" da them vao gio hang");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var items = await _cartRepository.GetCartItemsAsync(user.Id);
            return Ok(items);
        }
        [HttpDelete("{cartItemId}")]
        [Authorize]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var success = await _cartRepository.DeleteAsync(user.Id, cartItemId);
            if (!success) return NotFound(new { error = "Không tìm thấy món trong giỏ hàng" });

            return Ok(new { message = "Đã xóa món khỏi giỏ hàng" });
        }
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var success = await _cartRepository.UpdateCartItemQuantityAsync(user.Id, dto);
            if (!success) return NotFound(new { error = "Không tìm thấy món trong giỏ" });

            return Ok(new { message = "Đã cập nhật số lượng" });
        }
        [HttpGet("total")]
        [Authorize]
        public async Task<ActionResult<CartTotalDto>> GetCartTotal()
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var total = await _cartRepository.GetCartTotalAsync(user.Id);
            return Ok(total);
        }
        [HttpDelete("clear")]
        [Authorize]
        public async Task<IActionResult> ClearCart()
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var success = await _cartRepository.ClearCartAsync(user.Id);
            if (!success)
                return NotFound(new { message = "Giỏ hàng đã trống hoặc không tồn tại" });

            return Ok(new { message = "Đã xóa toàn bộ giỏ hàng" });
        }
        [HttpPut("select")]
        [Authorize]
        public async Task<IActionResult> SelectItems([FromBody] SelectedCartItemDto dto)
        {

            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var success = await _cartRepository.SetSelectedCartItemsAsync(user.Id, dto.CartItemIds, dto.IsSelected);
            if (!success)
                return NotFound(new { error = "Không tìm thấy các món cần chọn trong giỏ hàng" });

            return Ok(new { message = dto.IsSelected ? "Đã chọn món để thanh toán" : "Đã bỏ chọn món" });
        }


    }
}