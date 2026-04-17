using System;

namespace Cache
{
    ///<summary>
    ///Represents a thread-safe in-memory LRU cache with optional expiration and size-based eviction.
    ///</summary>
    public interface IQuickCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Retrieves the keys as an IEnumerable collection of type TKey. 
        /// </summary>
        IEnumerable<TKey> Keys { get; }
        /// <summary>
        /// Retrieves the values as an IEnumerable collection of type TValue.
        /// </summary>
        IEnumerable<TValue> Values { get; }
        /// <summary>
        /// Retrieves the total size of items in the cache.
        /// </summary>
        long Size { get; }
        /// <summary>
        /// Retrieves the maximum size of the cache.
        /// </summary>
        long MaxSize { get; }
        /// <summary>
        /// Attempts to retrieve a value from the cache. 
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The value if found.</param>
        /// <returns>True if found, otherwise false.</returns>
        bool TryGet(TKey key, out TValue value);
        /// <summary>
        /// Adds or updates a cache entry. 
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The value to be saved to the cache.</param>
        /// <param name="options">Optional cache entry configuration.</param>
        void Put(TKey key, TValue value, QuickCacheEntryOptions? options);
        /// <summary>
        /// Removes a specific key from the cache.
        /// </summary>
        /// <param name="key">The key to be removed.</param>
        /// <returns>True if key exists and is removed, otherwise false.</returns>
        bool Remove(TKey key);
        /// <summary>
        /// Clears the cache.
        /// </summary>
        void Clear();
    }
}
