using Custom.Memory.Cache.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Custom.Memory.Cache
{
    public class MemoryCache : IMemoryCache
    {
        private readonly ILogger<MemoryCache> _logger;
        /// <summary>
        /// Either from config file in consuming application or using IOptions we can inject
        /// </summary>
        private readonly int _maxCapacity;

        private LinkedList<KeyValuePair<string, string>> CacheItems { get; set; }

        public MemoryCache(ILogger<MemoryCache> logger, IConfiguration config)
        {
            _logger = logger;
            _maxCapacity = Convert.ToInt16(config.GetSection("CacheCapacity")?.Value);
            CacheItems = new();
        }

        /// <summary>
        /// Set items to cache
        /// </summary>
        /// <param name="cacheItem"></param>
        public void SetItem(string key, string value)
        {
            if (CacheItems.Count == _maxCapacity)
                CacheItems.RemoveFirst();
            RemoveIfExists(key);
            CacheItems.AddLast(new KeyValuePair<string, string>(key, value));
            _logger.LogInformation($"{key} added to cache successfully.");
        }

        /// <summary>
        /// For removing items from cache. Can return boolean as well if some operations needs to be performed based on this result
        /// </summary>
        /// <param name="key"></param>
        public void RemoveItem(string key)
        {
            if (RemoveIfExists(key))
                _logger.LogInformation($"{key} removed from cache successfully.");
            _logger.LogInformation($"{key} not present in cache");
        }

        /// <summary>
        /// ClearAll items from cache
        /// </summary>
        public void ClearAll()
        {
            CacheItems.Clear();
            _logger.LogInformation($"Cleared all items from the cache");
        }

        /// <summary>
        /// Get Items from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetItem(string key)
        {
            KeyValuePair<string, string> keyValuePair = new();
            _logger.LogInformation($"Trying to fetch key :{key}");
            foreach (var item in CacheItems)
            {
                if (item.Key == key)
                {
                    keyValuePair = item;
                    break;
                }
            }
            return keyValuePair;
        }

        #region PrivateMethods

        /// <summary>
        /// Already existing data if there is an update remove and add at last
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool RemoveIfExists(string key)
        {
            foreach (var item in CacheItems)
            {
                if (item.Key == key)
                {
                    CacheItems.Remove(item);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
