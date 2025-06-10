using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Models;

namespace Controllers{
    [Route("api/Cart")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase{
        private readonly AppDbContext _context;
        public CartController(AppDbContext context){
            _context = context;
        }

        //Thêm sản phẩm vào giỏ hàng
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest request){
            //Lấy userId từ Token
            var userId = GetUserIdFromToken();
            if (userId == null)
                return Unauthorized("Không tìm thấy người dùng.");
            
            //Lấy sản phẩm
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm.");
            
            //Kiểm tra xem sản phẩm có tồn tại trong giỏ hàng chưa
            var existingCart = await _context.Carts.FirstOrDefaultAsync(
                c => c.UserId == userId && c.ProductId == request.ProductId);
            if (existingCart != null){
                existingCart.Quantity += request.Quantity;
                _context.Carts.Update(existingCart);
            }else{
                var cart = new Cart{
                    UserId = (int)userId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    CreatedAt = DateTime.Now
                };
                _context.Carts.Add(cart);
            }
            await _context.SaveChangesAsync();
            return Ok(new {message = "Thêm hàng vào giỏ hàng thành công."});

        }

        private int? GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }
    }

    public class CartRequest{
        public int ProductId {get; set;}
        public int Quantity {get; set;}
    }
}