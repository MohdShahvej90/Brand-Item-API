using CRUDOperation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        // Injecting the BrandContext to interact with the database
        private readonly Models.BrandContext _context;
        public BrandController(BrandContext context)
        {
            _context = context;
        }

        // GET: api/Brand
        [HttpGet("GetBrands")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            return await _context.Brands.ToListAsync();
        }

        // GET: api/Brand/GetBrandById/5
        [HttpGet("GetBrandById/{id}")]
        public async Task<ActionResult<Brand>> GetBrandById([FromRoute] int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                return NotFound();

            return brand;
        }


        // CREATE: api/Brand
        [HttpPost("CreateBrand")]
        public async Task<ActionResult<Brand>> CreateBrand(Brand brand)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'BrandContext.Brands'  is null.");
            }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBrandById", new { id = brand.Id }, brand);
        }

        // UPDATE: api/Brand/UpdateBrand/5
        [HttpPut("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] int id,[FromBody] Brand brand) // Updated to use FromBody for brand parameter
        {
            if (id != brand.Id)
                return BadRequest("ID mismatch");

            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Brand updated successfully");
        }


        // DELETE: api/Brand/DeleteBrandById/5
        [HttpDelete("DeleteBrandById/{id}")]
        public async Task<IActionResult> DeleteBrandById(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                return NotFound();

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
