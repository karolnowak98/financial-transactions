using StackExchange.Redis;

namespace server.Infrastructure.Cache;

public class RedisController(IConnectionMultiplexer redis) : ICacheController
{
    public async Task<string> GetValueFromRedis(string key)
    {
        var db = redis.GetDatabase();
        var value = await db.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            return "";
        }
        
        return value;
    }
    
    public async Task SetValueInRedis(string key, string value)
    {
        var db = redis.GetDatabase();
        await db.StringSetAsync(key, value);
    }
}