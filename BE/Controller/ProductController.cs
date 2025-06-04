using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYWEBAPI.Models; 
using MYWEBAPI.Data; // ✅ Đúng theo namespace bạn đã định nghĩa
namespace Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Categories)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Products request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Nếu cần kiểm tra CategoryId hợp lệ:
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null)
                return BadRequest("CategoryId không hợp lệ!");

            var product = new Products
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Instock = request.Instock,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.UtcNow
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Lấy lại thông tin Category để trả về đầy đủ
            product.Categories = category;

            return Ok(product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Products request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            // Nếu muốn kiểm tra CategoryId:
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null)
                return BadRequest("CategoryId không hợp lệ!");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Instock = request.Instock;
            product.ImageUrl = request.ImageUrl;
            product.CategoryId = request.CategoryId;
            // product.CreatedAt giữ nguyên

            await _context.SaveChangesAsync();

            // Lấy lại thông tin Category để trả về đầy đủ
            product.Categories = category;

            return Ok(product);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa sản phẩm thành công." });
        }
    }
}