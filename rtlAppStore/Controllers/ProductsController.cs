using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rtlAppStore.Data;
using rtlAppStore.Entities;

namespace rtlAppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private StoreContext _storeContext;
        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _storeContext.Products.ToListAsync();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int Id)
        {
            var product = await _storeContext.Products.FindAsync(Id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProducts(Product product)
        {

            _storeContext.Products.Add(product);
            await _storeContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> UpdateProduct(int Id,Product product)
        {
            if (Id != product.Id || !ExistProduct(Id))
                return BadRequest("product doesn't exist");

            _storeContext.Entry(product).State = EntityState.Modified;

            await _storeContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            var product = await _storeContext.Products.FindAsync(Id);
            if (product == null) return NotFound();

            _storeContext.Products.Remove(product);
            await _storeContext.SaveChangesAsync();
            return NoContent();
        }

        private bool ExistProduct(int Id)
        {
            return _storeContext.Products.Any(x => x.Id == Id);
        }

    }
}
