# QuickCache
A memory cache that evicts based on LRU, time, and size.

QuickCache is a lightweight, thread-safe in-memory cache for .NET that supports:/n
Least Recently Used (LRU) eviction/n
Absolute and sliding expiration/n
Size-based eviction/n
Background cleanup/n
Bulk insert support for high-performance scenarios/n

Features
Thread-safe using internal locking/n
LRU eviction via linked list tracking/n
Configurable size limits to prevent memory overuse/n
Expiration support/n
Absolute expiration/n
Sliding expiration/n
Bulk operations to reduce lock contention/n
Background cleanup using a timer/n

Installation
To install, clone the repository/n

git clone https://github.com/janzenhouchenwilder/QuickCache.git

To use/n
var cache = new QuickCache<string, string>(10000);/n
or for dependency injection/n
services.AddSingleton<IQuickCache<string, string>, QuickCache<int, int>>();/n
If you want to set the size of the cache\n
builder.Services.AddSingleton<IQuickCache<int, Person>>(options =>
{
    return new QuickCache<int, Person>(50000, 20000);
});

Add or update an item/n
cache.Put("key", "value", new QuickCacheEntryOptions { Size = 1, AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });/n

Retrieve an item/n
if (cache.TryGet("key", out var value)) { Console.WriteLine(value); }/n

Remove an item/n
if (cache.TryGet("key", out var value)) { Console.WriteLine(value); }/n

Insert multiple items (Large inserts)/n
cache.PutMany(items);/n

Performance Notes/n
Use PutMany for bulk inserts to avoid lock contention./n
Designed for high-throughput with minimal allocations./n

Limitations/n
In-memory only, not distributed/n
Global lock may cause contention under extreme parallel locks/n

