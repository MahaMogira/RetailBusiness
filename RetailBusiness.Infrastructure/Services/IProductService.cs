using RetailBusiness.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetAllProductsNoTrackingAsync();
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
