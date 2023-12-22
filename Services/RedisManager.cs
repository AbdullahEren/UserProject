using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Services.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services
{
    public class RedisManager : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private static ConcurrentDictionary<string, bool> CacheKeys = new();

        public RedisManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key);
            if (cachedValue is null)
            {
                return null;
            }
            T? value = JsonConvert.DeserializeObject<T>(cachedValue);

            return value;
        }
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            T? cachedValue = await GetAsync<T>(key);
            if (cachedValue is not null)
            {
                return cachedValue;
            }
            cachedValue = await action();
            await SetAsync(key, cachedValue);
            return cachedValue;
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            var serializerOptions = new System.Text.Json.JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true // İhtiyaca bağlı olarak
            };

            string cacheValue = System.Text.Json.JsonSerializer.Serialize(value, serializerOptions);

            await _distributedCache.SetStringAsync(key, cacheValue);

            CacheKeys.TryAdd(key, false);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);

            CacheKeys.TryRemove(key, out bool _);
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            foreach(string key in CacheKeys.Keys)
            {
                await RemoveAsync(key);
            }
        }

    }
}
