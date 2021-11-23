using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Custom.Memory.Cache.Tests
{
    /// <summary>
    /// Below properties and setup can be done in separate IClassFixture<MemoryCacheFixture>
    /// </summary>
    public class MemoryCacheTest
    {
        private readonly Mock<ILogger<MemoryCache>> Logger;
        private readonly MemoryCache MemoryCache;
        private readonly Mock<IConfiguration> Config;
        private const string Key = "SampleKey";
        private const string Value = "SampleKey";

        public MemoryCacheTest()
        {
            Logger = new();
            Config = new();
            Config.Setup(x => x.GetSection(It.IsAny<string>()).Value).Returns("2");
            MemoryCache = new(Logger.Object, Config.Object);
        }

        [Fact]
        public void GivenKeyValueWhenInvokeRemoveItemThenItemShouldRemoveFromCache()
        {
            MemoryCache.ClearAll();//Setup added if other test case adds/removes items from cache

            MemoryCache.SetItem(Key, Value);
            MemoryCache.RemoveItem(Key);
            var item = MemoryCache.GetItem(Key);
            Assert.Equal(default,item);
        }

        [Fact]
        public void GivenKeyWhenInvokeGetItemAndItemPresentInCacheThenItemShouldFetchItemFromCache()
        {
            MemoryCache.ClearAll();//Setup added if other test case adds/removes items from cache

            MemoryCache.SetItem(Key, Value);
            var result = MemoryCache.GetItem(Key);
            Assert.Equal(Value, result.Value);
        }

        [Fact]
        public void GivenKeyValueWhenInvokeSetItemAndItemPresentInCacheThenItemShouldUpdateCache()
        {
            MemoryCache.ClearAll();//Setup added if other test case adds/removes items from cache

            MemoryCache.SetItem(Key, Value);
            MemoryCache.SetItem(Key, "updated value");
            var result = MemoryCache.GetItem(Key);
            Assert.Equal("updated value", result.Value);
        }

        [Fact]
        public void GivenKeyValueWhenInvokedSetItemAndReachesThreshholdThenFirstItemShouldRemoveFirstFromCache()
        {
            MemoryCache.ClearAll();//Setup added if other test case adds/removes items from cache

            MemoryCache.SetItem(Key, Value);
            MemoryCache.SetItem("SampleKey1", "SampleValue1");
            MemoryCache.SetItem("SampleKey2", "SampleValue2");
            var item = MemoryCache.GetItem(Key);
            Assert.Equal(default, item);
        }
    }
}
