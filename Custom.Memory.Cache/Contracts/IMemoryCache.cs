using System.Collections.Generic;

namespace Custom.Memory.Cache.Contracts
{
    /// <summary>
    /// Need to configure the threshhold in consuming project
    /// </summary>
    public interface IMemoryCache
    {
        /// <summary>
        /// To get the Item from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        KeyValuePair<string, string> GetItem(string key);
        /// <summary>
        /// Set the item into cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetItem(string key, string value);
        /// <summary>
        /// For clearing all items from the cache
        /// </summary>
        void ClearAll();
    }
}