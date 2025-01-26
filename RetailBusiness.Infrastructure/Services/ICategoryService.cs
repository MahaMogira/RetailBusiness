using RetailBusiness.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
