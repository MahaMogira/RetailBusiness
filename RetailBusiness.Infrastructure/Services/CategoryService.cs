using RetailBusiness.Core.Entities;
using RetailBusiness.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ISqlExecutor _sqlExecutor;

        public CategoryService(ISqlExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            string sql = "EXEC GetProductsByCategory @CategoryId = {0}";
            return await _sqlExecutor.ExecuteSqlQueryAsync<Product>(sql, categoryId);
        }
    }
}
