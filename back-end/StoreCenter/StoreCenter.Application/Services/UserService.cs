using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Dtos;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class UserService : IUserService
    {

        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        //public async Task<(bool Success, List<string> Errors)> AddCategoryAsync(Category category)
        //{
        //    var errors = new List<string>();
        //    try
        //    {
        //        await _categoryRepository.AddCategory(category);
        //        return (true, errors);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //        return (false, errors);
        //    }
        //}

        //public async Task<(bool Success, List<string> Errors)> DeleteCategoryAsync(Guid categoryId)
        //{
        //    var errors = new List<string>();
        //    try
        //    {
        //        await _categoryRepository.DeleteCategory(categoryId);
        //        return (true, errors);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //        return (false, errors);
        //    }
        //}

        public async Task<(bool Success, List<string> Errors, IEnumerable<UserDto?> userDtos)> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var userDtos = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                });

                return (true, new List<string>(), userDtos);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, Enumerable.Empty<UserDto?>());
            }
        }

        //public async Task<(bool Success, List<string> Errors, Category? Category)> GetCategoryByIdAsync(Guid categoryId)
        //{
        //    try
        //    {
        //        var category = await _categoryRepository.GetCategory(categoryId);
        //        return (true, new List<string>(), category);
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, new List<string> { ex.Message }, null);
        //    }
        //}

        //public async Task<(bool Success, List<string> Errors)> UpdateCategoryAsync(Category category)
        //{
        //    var errors = new List<string>();
        //    try
        //    {
        //        await _categoryRepository.UpdateCategory(category);
        //        return (true, errors);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //        return (false, errors);
        //    }
        //}

    }
}
