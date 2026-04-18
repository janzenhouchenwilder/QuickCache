# QuickCache
A memory cache that evicts based on LRU, time, and size.

QuickCache is a lightweight, thread-safe in-memory cache for .NET that supports:<br>

Least Recently Used (LRU) eviction<br>
Absolute and sliding expiration<br>
Size-based eviction<br>
Background cleanup<br>
Bulk insert support for high-performance scenarios

__Features__<br>
Thread-safe using internal locking<br>
LRU eviction via linked list tracking<br>
Configurable size limits to prevent memory overuse<br>
Expiration support<br>
Absolute expiration<br>
Sliding expiration<br>
Bulk operations to reduce lock contention<br>
Background cleanup using a timer

__Installation__
To install, clone the repository

```git clone https://github.com/janzenhouchenwilder/QuickCache.git```

__To use__<br>
```var cache = new QuickCache<string, string>(10000);```<br>
or for dependency injection<br>
```services.AddSingleton<IQuickCache<string, string>, QuickCache<int, int>>();```<br>
If you want to set the size of the cache<br>
```
builder.Services.AddSingleton<IQuickCache<int, Person>>(options =>
{
    return new QuickCache<int, Person>(50000, 20000);
});
```

__Add or update an item__<br>
```cache.Put("key", "value", new QuickCacheEntryOptions { Size = 1, AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });```

__Retrieve an item__<br>
```if (cache.TryGet("key", out var value)) { Console.WriteLine(value); }```

__Remove an item__<br>
```if (cache.TryGet("key", out var value)) { Console.WriteLine(value); }```

__Insert multiple items (Large inserts)__<br>
```cache.PutMany(items);```

__Performance Notes__<br>
Use `PutMany` for bulk inserts to avoid lock contention.<br>
Designed for high-throughput with minimal allocations.

Benchmark Dotnet for QuickCache `Put(TKey key, TValue, QuickCacheEntryOptions? options)` method.<br>
| Method | Mean     | Error     | StdDev    | Gen0     | Gen1     | Allocated |
|------- |---------:|----------:|----------:|---------:|---------:|----------:|
| Put    | 3.802 ms | 0.0753 ms | 0.1451 ms | 578.1250 | 570.3125 |   3.48 MB |

Run time: 00:00:33 (33.73 sec), executed benchmarks: 1

Benchmark Dotnet for MemoryCache `Set(object key, TItem value, MemoryCacheEntryOptions? options)` method.<br>
| Method | Mean     | Error     | StdDev    | Gen0     | Gen1     | Allocated |
|------- |---------:|----------:|----------:|---------:|---------:|----------:|
| Set    | 4.530 ms | 0.0906 ms | 0.2449 ms | 695.3125 | 687.5000 |   4.17 MB |

Run time: 00:01:00 (60.23 sec), executed benchmarks: 1

Benchmark Dotnet for QuickCache `Put(TKey key, TValue, QuickCacheEntryOptions? options)` method.<br>
| Method | Mean     | Error     | StdDev    | Allocated |
|------- |---------:|----------:|----------:|----------:|
| TryGet | 1.274 ms | 0.0250 ms | 0.0470 ms |      40 B |

Run time: 00:00:41 (41.65 sec), executed benchmarks: 1

Benchmark Dotnet for MemoryCache `TryGetValue(object key, out TItem value)` method.<br>
| Method | Mean     | Error    | StdDev   | Gen0    | Allocated |
|------- |---------:|---------:|---------:|--------:|----------:|
| TryGet | 821.1 us | 16.18 us | 34.12 us | 69.3359 | 427.38 KB |

Run time: 00:00:57 (57.74 sec), executed benchmarks: 1

These tests were conducted by querying a database with 18000 rows of data and adding/retrieving from the cache.

__Limitations__<br>
In-memory only, not distributed<br>
Global lock may cause contention under extreme parallel locks

