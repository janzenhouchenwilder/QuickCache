using System;

namespace LightMemCache
{
    /// <summary>
    /// Configuration for a cache entry.
    /// </summary>
    public class LightCacheEntryOptions
    {
        /// <summary>
        /// The relative time the entry expires.
        /// </summary>
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        /// <summary>
        /// The sliding expiration window.
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }
        /// <summary>
        /// The size of the entry.
        /// </summary>
        public long Size { get; set; } = 1;
    }
}
