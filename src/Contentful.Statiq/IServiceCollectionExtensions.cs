using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Contentful.Statiq
{
    /// <summary>
    /// Extension methods for IServiceCollection.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        private const string HttpClientName = "ContentfulClient";

        /// <summary>
        /// Adds Contentful services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection.</param>
        /// <param name="configuration">The IConfigurationRoot used to retrieve configuration from.</param>
        /// <returns>The IServiceCollection.</returns>
        public static IServiceCollection AddContentful(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return AddContentful(services, (IConfiguration) configuration);
        }

        /// <summary>
        /// Adds Contentful services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection.</param>
        /// <param name="configuration">The IConfiguration used to retrieve configuration from.</param>
        /// <returns>The IServiceCollection.</returns>
        public static IServiceCollection AddContentful(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ContentfulOptions>(configuration.GetSection("ContentfulOptions"));
            services.AddHttpClient(HttpClientName);
            services.TryAddTransient<IContentfulClient>((sp) =>
            {
                var options = sp.GetService<IOptions<ContentfulOptions>>()?.Value;
                var factory = sp.GetService<IHttpClientFactory>();
                var httpClient = factory?.CreateClient(HttpClientName);
                return new ContentfulClient(httpClient, options);
            });

            return services;
        }
    }
}