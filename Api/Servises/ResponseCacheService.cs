using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Servises
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive)
        {
            if (response == null)
            {
                return;
            }

            var serializeResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(cacheKey, serializeResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeTimeLive
            });
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheKey) ? null : cachedResponse;
        }
    }
}
