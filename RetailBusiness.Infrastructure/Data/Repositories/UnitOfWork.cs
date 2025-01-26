using RetailBusiness.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Category> _categoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> ProductRepository => _productRepository ??= new GenericRepository<Product>(_context);
        public IGenericRepository<Category> CategoryRepository => _categoryRepository ??= new GenericRepository<Category>(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
