using Microsoft.AspNetCore.Mvc;
using StoreCenter.Application.Dtos;
using StoreCenter.Application.Interfaces;

namespace StoreCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(Guid id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDto>> CreateBrand(CreateBrandDto createBrandDto)
        {
            if (await _brandService.BrandExistsAsync(createBrandDto.Name))
                return BadRequest($"Brand with name '{createBrandDto.Name}' already exists.");

            var brand = await _brandService.CreateBrandAsync(createBrandDto);
            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BrandDto>> UpdateBrand(Guid id, UpdateBrandDto updateBrandDto)
        {
            var brand = await _brandService.UpdateBrandAsync(id, updateBrandDto);
            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}