﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(
    Expression<Func<T, bool>> predicate,
    bool disableTracking = false);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
