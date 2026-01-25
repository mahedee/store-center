using StoreCenter.Application.Interfaces;
using StoreCenter.Infrastructure.Interfaces;
using AutoMapper;
using StoreCenter.Domain.Entities;
using StoreCenter.Application.Dtos;

namespace StoreCenter.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<BrandDto?> GetBrandByIdAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            return brand == null ? null : _mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var brand = _mapper.Map<Brand>(createBrandDto);
            brand.Id = Guid.NewGuid();
            brand.CreatedAt = DateTime.UtcNow;

            await _brandRepository.AddAsync(brand);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto?> UpdateBrandAsync(Guid id, UpdateBrandDto updateBrandDto)
        {
            var existingBrand = await _brandRepository.GetByIdAsync(id);
            if (existingBrand == null)
                return null;

            _mapper.Map(updateBrandDto, existingBrand);
            existingBrand.UpdatedAt = DateTime.UtcNow;

            await _brandRepository.UpdateAsync(existingBrand);
            return _mapper.Map<BrandDto>(existingBrand);
        }

        public async Task<bool> DeleteBrandAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return false;

            await _brandRepository.DeleteAsync(brand);
            return true;
        }

        public async Task<bool> BrandExistsAsync(string name)
        {
            var brand = await _brandRepository.GetByNameAsync(name);
            return brand != null;
        }
    }
}