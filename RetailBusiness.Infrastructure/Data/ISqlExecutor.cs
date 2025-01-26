using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Infrastructure.Data
{
    public interface ISqlExecutor
    {
        Task<IEnumerable<T>> ExecuteSqlQueryAsync<T>(string sql, params object[] parameters) where T : class;
    }
}
