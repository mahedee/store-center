﻿using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAllUsersAsync();
            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }
            return ApiResponseHelper.Success(result.userDtos, "Users retrieved successfully");
        }

        //// GET api/<CategoriesController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var result = await _categoryService.GetCategoryByIdAsync(id);
        //    if (!result.Success || result.Category is null)
        //    {
        //        return ApiResponseHelper.NotFound("Category not found");
        //    }
        //    return ApiResponseHelper.Success(result.Category);
        //}

        //// POST api/<CategoriesController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Category category)
        //{
        //    var result = await _categoryService.AddCategoryAsync(category);

        //    if (!result.Success)
        //    {
        //        return ApiResponseHelper.ValidationError(result.Errors);
        //    }

        //    return ApiResponseHelper.Success(null, "Category created successfully");
        //}

        //// PUT api/<CategoriesController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(Guid id, [FromBody] Category category)
        //{
        //    if(id != category.Id)
        //    {
        //        return ApiResponseHelper.Error("Category ID mismatch");
        //    }

        //    var result = await _categoryService.GetCategoryByIdAsync(id);
        //    if (!result.Success || result.Category is null)
        //    {
        //        return ApiResponseHelper.NotFound("Category not found");
        //    }

        //    category.Id = id;
        //    await _categoryService.UpdateCategoryAsync(category);
        //    return ApiResponseHelper.Success(null, "Category updated successfully");
        //}

        //// DELETE api/<CategoriesController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _categoryService.GetCategoryByIdAsync(id);
        //    if (!result.Success || result.Category is null)
        //    {
        //        return ApiResponseHelper.NotFound("Category not found");
        //    }

        //    var deleteResult = await _categoryService.DeleteCategoryAsync(id);
        //    if (!deleteResult.Success)
        //    {
        //        return ApiResponseHelper.ValidationError(deleteResult.Errors);
        //    }

        //    return ApiResponseHelper.Success(null, "Category deleted successfully");
        //}
    }
}
