using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Api.Models;
using StoreCenter.Application.Common.Exceptions;
using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// 
// ENHANCED ERROR HANDLING:
// - Uses RFC 7807 compliant error responses via GlobalExceptionHandlingMiddleware
// - Throws domain-specific exceptions that are handled globally
// - Includes structured logging with correlation IDs
// - Returns consistent error format across all endpoints

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // Example: api/Categories?PageNumber=1&PageSize=100&SearchTerm=Books&SearchField=Name&OrderBy=Name&IsDescending=false
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationOptions paginationOptions)
        {
            _logger.LogInformation("Fetching categories with pagination options: {@PaginationOptions}", paginationOptions);
            
            // Call the service to get the paginated categories
            var result = await _categoryService.GetAllCategoriesAsync(paginationOptions);

            // If the result is not successful, throw validation exception
            if (!result.Success)
            {
                var errors = new Dictionary<string, string[]>
                {
                    { "General", result.Errors.ToArray() }
                };
                throw new ValidationException(errors);
            }

            // Return the result wrapped in a success response
            return Ok(result);
        }


        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation("Fetching category with ID: {CategoryId}", id);
            
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (!result.Success || result.Category is null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            return Ok(result.Category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequest request)
        {
            _logger.LogInformation("Creating category with name: {CategoryName}", request.Name);

            var category = new Category 
            { 
                Name = request.Name, 
                Description = request.Description 
            };

            // Validate the category
            category.ValidateForCreation();

            var result = await _categoryService.AddCategoryAsync(category);

            if (!result.Success)
            {
                var errors = new Dictionary<string, string[]>
                {
                    { "General", result.Errors.ToArray() }
                };
                throw new ValidationException(errors);
            }

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCategoryRequest request)
        {
            _logger.LogInformation("Updating category with ID: {CategoryId}", id);

            var existingResult = await _categoryService.GetCategoryByIdAsync(id);
            if (!existingResult.Success || existingResult.Category is null)
            {
                throw new NotFoundException(nameof(Category), id);
            }

            var category = new Category 
            { 
                Id = id,
                Name = request.Name, 
                Description = request.Description 
            };

            // Validate the category
            category.ValidateForUpdate();
            
            var updateResult = await _categoryService.UpdateCategoryAsync(category);
            if (!updateResult.Success)
            {
                var errors = new Dictionary<string, string[]>
                {
                    { "General", updateResult.Errors.ToArray() }
                };
                throw new ValidationException(errors);
            }

            return Ok(category);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting category with ID: {CategoryId}", id);

            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (!result.Success || result.Category is null)
            {
                throw new NotFoundException(nameof(Category), id);
            }

            var deleteResult = await _categoryService.DeleteCategoryAsync(id);
            if (!deleteResult.Success)
            {
                var errors = new Dictionary<string, string[]>
                {
                    { "General", deleteResult.Errors.ToArray() }
                };
                throw new ValidationException(errors);
            }

            return NoContent();
        }
    }
}
