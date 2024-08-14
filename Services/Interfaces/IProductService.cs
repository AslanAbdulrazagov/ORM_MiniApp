using ORM_MiniApp.DTOs.Product;

namespace ORM_MiniApp.Services.Interfaces
{
    public interface IProductService
    {
        Task AddProductAsync(ProductPostDto product);
        Task UpdateProductAsync(ProductGetDto product);
        Task DeleteProductAsync(int id);
        Task<List<ProductGetDto>> GetProductsAsync();
        Task<ProductGetDto> GetProductByIdAsync(int id);
        Task<List<ProductGetDto>> SearchProducts(string name);
    }
}
