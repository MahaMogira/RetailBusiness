using RetailBusiness.Core.Entities;
using RetailBusiness.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync(int page, int pageSize)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return products.Skip((page - 1) * pageSize).Take(pageSize);

        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.ProductRepository.FindAsync(p => p.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsNoTrackingAsync()
        {
            return await _unitOfWork.ProductRepository.FindAsync(p => true, disableTracking: true);
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            var result = await _unitOfWork.SaveAsync();
            return result > 0; // Returns true if one or more rows are affected
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            _unitOfWork.ProductRepository.Delete(product);
            var result = await _unitOfWork.SaveAsync();
            return result > 0; // Returns true if one or more rows are affected
        }


    }
}
