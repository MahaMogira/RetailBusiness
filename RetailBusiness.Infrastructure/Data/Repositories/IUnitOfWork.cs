using RetailBusiness.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        Task<int> SaveAsync();
    }
}
