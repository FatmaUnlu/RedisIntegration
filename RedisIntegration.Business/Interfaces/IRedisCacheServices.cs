using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntegration.Business.Interfaces
{
    public interface IRedisCacheServices
    {
        Task ClearKeyAsync(string key);
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        void ClearAll();
        Task<bool> KeyExistsAsync(string key);
        Task<bool> SetValueWithExpiryAsync(string key, string value, TimeSpan expiry);
        Task AddToListAsync(string key, string value);
        Task<IEnumerable<string>> GetListAsync(string key);
    }
}
