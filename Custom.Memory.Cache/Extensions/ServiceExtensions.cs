using Custom.Memory.Cache.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Custom.Memory.Cache.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add this method in the consuming project
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomCacheSerive(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMemoryCache, MemoryCache>();
            return services;
        }
    }
}
