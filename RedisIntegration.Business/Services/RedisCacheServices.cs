using RedisIntegration.Business.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIntegration.Business.Services
{
    public class RedisCacheServices :IRedisCacheServices
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        public RedisCacheServices(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
        }
        public async Task ClearKeyAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
        public async Task<string> GetValueAsync(string key)
        {
             return await _database.StringGetAsync(key);
        }
        public async Task<bool> SetValueAsync(string key,string value)
        {
            return await _database.StringSetAsync(key,value);
        }
        public void ClearAll()
        {
            var redisEndPoints = _connectionMultiplexer.GetEndPoints();
            foreach (var redisEndPoint in redisEndPoints)
            {
                var redisServer = _connectionMultiplexer.GetServer(redisEndPoint);
                redisServer.FlushAllDatabases();
            }
        }
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
        //bir süreliğine bir key oluşturmak için
        public async Task<bool> SetValueWithExpiryAsync(string key, string value, TimeSpan expiry)
        {
            return await _database.StringSetAsync(key, value, expiry);
        }
        public async Task AddToListAsync(string key, string value)
        {
            await _database.ListRightPushAsync(key, value);
        }
        public async Task<IEnumerable<string>> GetListAsync(string key)
        {
            var listLength = await _database.ListLengthAsync(key);
            var values = await _database.ListRangeAsync(key, 0, listLength - 1);
            return values.Select(v => v.ToString());
        }
    }
}
