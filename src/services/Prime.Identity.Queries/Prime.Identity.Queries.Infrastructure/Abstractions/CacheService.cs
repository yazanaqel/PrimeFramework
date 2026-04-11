using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Queries.Infrastructure.Abstractions;

using Microsoft.Extensions.Caching.Distributed;
using Prime.Identity.Queries.Application.Abstractions.Cache;
using System.Text.Json;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken = default)
    {
        var data = await _cache.GetStringAsync(key,cancellationToken);
        if(string.IsNullOrEmpty(data))
            return default;

        return JsonSerializer.Deserialize<T>(data,_jsonOptions);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiry = null,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value,_jsonOptions);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(10)
        };

        await _cache.SetStringAsync(key,json,options,cancellationToken);
    }

    public Task RemoveAsync(string key,CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(key,cancellationToken);
}
