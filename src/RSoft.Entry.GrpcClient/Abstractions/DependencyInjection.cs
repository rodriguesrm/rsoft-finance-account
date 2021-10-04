using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Entry.GrpcClient.Options;

namespace RSoft.Entry.GrpcClient.Abstractions
{

    /// <summary>
    /// Dependency injection class
    /// </summary>
    public static class DependencyInjection
    {

        /// <summary>
        /// Register services from EntryGrpcService Client
        /// </summary>
        /// <param name="services">Service colleciton instance</param>
        /// <param name="configuration">Configuration object instance</param>
        public static IServiceCollection AddEntryGrpcServiceClient(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EntryServiceHostOption>(options => configuration.GetSection("EntryGrpcService:Server").Bind(options));

            services.AddScoped<IGrpcChannelFactory, GrpcChannelFactory>();

            return services;
        }

    }

}
