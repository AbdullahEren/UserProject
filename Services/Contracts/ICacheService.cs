using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task<T> GetAsync<T>(string key, Func<Task<T>> action) where T : class;
        Task SetAsync<T>(string key, T value) where T : class;
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string prefix);
    }
}
