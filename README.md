# Memory.Cache
Generic memory cache integration guidelines
1. Add the key "CacheCapacity" and value to appsettings.json in the consuming project - This indicates the number of items that can be stored in the cache based on the available memory.
2. Add "services.AddCustomCacheSerive" in the Starup.	(namespace: using Custom.Memory.Cache.Extensions)