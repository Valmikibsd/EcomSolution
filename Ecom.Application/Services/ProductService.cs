using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;

namespace Ecom.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repo;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Product> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetAllAsync());

    public async Task<ProductDto?> GetByIdAsync(int id)
        => _mapper.Map<ProductDto?>(await _repo.GetByIdAsync(id));

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException("Product not found");

        _mapper.Map(dto, existing);
        _repo.Update(existing);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException("Product not found");
        _repo.Delete(existing);
        await _repo.SaveChangesAsync();
    }
}
