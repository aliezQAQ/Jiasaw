using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Jiasaw.Utils
{
    internal static class MemCache
    {
        private static ObjectCache cache = MemoryCache.Default;

        internal static void Add(string cacheKey,object cacheValue,DateTime dateTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = dateTime;
            cache.Add(cacheKey, cacheValue, policy);
        }
        internal static void Add(string cacheKey, object cacheValue)
        {
            cache.Add(cacheKey, cacheValue,null);
        }
        internal static object Get(string cacheKey)
        {
             return cache.Get(cacheKey);
        }
        internal static void Remove(string cacheKey)
        {
            cache.Remove(cacheKey);
        }
    }
}
