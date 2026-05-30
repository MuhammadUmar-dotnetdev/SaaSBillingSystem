using SaaSBillingSystem.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class RedisCacheService: ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            if(value.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var json = JsonSerializer.Serialize(value);

            await _database.StringSetAsync(key, json, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
