using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Data
{
    public class SqlExecutor : ISqlExecutor
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlExecutor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> ExecuteSqlQueryAsync<T>(string sql, params object[] parameters) where T : class
        {
            return await _dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}
