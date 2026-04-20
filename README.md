# LightCache
A memory cache that evicts based on LRU, time, and size.

LightCache is a lightweight, thread-safe in-memory cache for .NET that supports:

- Least Recently Used (LRU) eviction
- Absolute and sliding expiration
- Size-based eviction
- Background cleanup
- Bulk insert support for high-performance scenarios

## Features

- Thread-safe using internal locking
- LRU eviction via linked list tracking
- Configurable size limits to prevent memory overuse
- Expiration support
  - Absolute expiration
  - Sliding expiration
- Bulk operations to reduce lock contention
- Background cleanup using a timer

## Installation

To install, clone the repository:

```
git clone https://github.com/janzenhouchenwilder/QuickCache.git
```
Usage
```
var cache = new LightCache<string, string>(10000);
```

Or for dependency injection:
```
services.AddSingleton<ILightCache<string, string>, LightCache<int, int>>();
```

If you want to set the size of the cache:

```
builder.Services.AddSingleton<ILightCache<int, Person>>(options =>
{
    return new LightCache<int, Person>(50000, 20000);
});
```
Add or update an item
```
cache.Put("key", "value", new LightCacheEntryOptions 
{ 
    Size = 1, 
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) 
});
```
Retrieve an item
```
if (cache.TryGet("key", out var value)) 
{ 
    Console.WriteLine(value); 
}
```
Remove an item
```
if (cache.TryGet("key", out var value)) 
{ 
    Console.WriteLine(value); 
}
```
Insert multiple items (Large inserts)
```
cache.PutMany(items);
```

Performance Notes

Use `PutMany` for bulk inserts to avoid lock contention.
Designed for high-throughput with minimal allocations.

Benchmark Dotnet for LightCache `Put(TKey key, TValue, LightCacheEntryOptions? options)` method.

| Method | Mean     | Error     | StdDev    | Gen0     | Gen1     | Allocated |
|------- |---------:|----------:|----------:|---------:|---------:|----------:|
| Put    | 3.802 ms | 0.0753 ms | 0.1451 ms | 578.1250 | 570.3125 |   3.48 MB |

Run time: 00:00:33 (33.73 sec), executed benchmarks: 1

Benchmark Dotnet for MemoryCache `Set(object key, TItem value, MemoryCacheEntryOptions? options)` method.

| Method | Mean     | Error     | StdDev    | Gen0     | Gen1     | Allocated |
|------- |---------:|----------:|----------:|---------:|---------:|----------:|
| Set    | 4.530 ms | 0.0906 ms | 0.2449 ms | 695.3125 | 687.5000 |   4.17 MB |

Run time: 00:01:00 (60.23 sec), executed benchmarks: 1

Benchmark Dotnet for LightCache `TryGet(TKey key, out TValue value)` method.

| Method | Mean     | Error     | StdDev    | Allocated |
|------- |---------:|----------:|----------:|----------:|
| TryGet | 1.274 ms | 0.0250 ms | 0.0470 ms |      40 B |

Run time: 00:00:41 (41.65 sec), executed benchmarks: 1

Benchmark Dotnet for MemoryCache `TryGetValue(object key, out TItem value)` method.

| Method | Mean     | Error    | StdDev   | Gen0    | Allocated |
|------- |---------:|---------:|---------:|--------:|----------:|
| TryGet | 821.1 us | 16.18 us | 34.12 us | 69.3359 | 427.38 KB |

Run time: 00:00:57 (57.74 sec), executed benchmarks: 1

These tests were conducted by querying a database with 18000 rows of data and adding/retrieving from the cache.

Limitations
In-memory only, not distributed
Global lock may cause contention under extreme parallel loads