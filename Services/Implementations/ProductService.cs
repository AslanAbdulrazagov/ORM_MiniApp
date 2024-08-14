using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.DTOs.Product;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Abstractions;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(ProductPostDto newProduct)
        {

            Product product = new Product()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Description = newProduct.Description,
                Stock = newProduct.Stock,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {

            var product = await _productRepository.GetSingleAsync(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{id}");
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ProductGetDto> GetProductByIdAsync(int id)
        {

            var product = await _productRepository.GetSingleAsync(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{id}");
            var productDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
            return productDto;
        }

        public async Task<List<ProductGetDto>> GetProductsAsync()
        {

            var products = await _productRepository.GetAllAsync().ToListAsync();

            var productDtos = products.Select(product => new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
            }).ToList();
            return productDtos;
        }

        public async Task<List<ProductGetDto>> SearchProducts(string name)
        {
            
            var products = await _productRepository.GetFilterAsync(p => p.Name.Contains(name));
            var productDtos = products.Select(product => new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            }).ToList();
            return productDtos;

        }

        public async Task UpdateProductAsync(ProductGetDto product)
        {
            var productDb = await _productRepository.GetSingleAsync(p => p.Id == product.Id);
            if (productDb == null)
                throw new NotFoundException($"Can find product with id:{product.Id}");
            var productDto = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                UpdatedDate = DateTime.UtcNow
            };
            _productRepository.Update(productDto);
            await _productRepository.SaveChangesAsync();
        }
    }
}
