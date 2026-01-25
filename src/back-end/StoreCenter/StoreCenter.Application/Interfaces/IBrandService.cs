using StoreCenter.Application.Dtos;

namespace StoreCenter.Application.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<BrandDto?> GetBrandByIdAsync(Guid id);
        Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto);
        Task<BrandDto?> UpdateBrandAsync(Guid id, UpdateBrandDto updateBrandDto);
        Task<bool> DeleteBrandAsync(Guid id);
        Task<bool> BrandExistsAsync(string name);
    }
}