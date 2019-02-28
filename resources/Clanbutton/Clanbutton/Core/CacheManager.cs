using System.Collections.Generic;

using Android.Support.V7.App;

using Clanbutton.Builders;
using Clanbutton.Activities;
using System;
using System.Net;
using Android.Widget;
using Microsoft.Extensions.Caching.Memory;

namespace Clanbutton.Core
{
    public class CacheManager
    {
        private readonly IMemoryCache _cache;

        public CacheManager()
        {
            _cache = new MemoryCache(new MemoryCacheOptions() { });
        }

        public void Set<T>(string key, T value, DateTimeOffset absoluteExpiry)
        {
            _cache.Set(key, value, absoluteExpiry);
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
                return value;
            else
                return default(T);
        }
    }
}