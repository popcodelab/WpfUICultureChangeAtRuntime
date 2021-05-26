using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using WpfUICultureChangeAtRuntime.Localization.Json;

namespace WpfUICultureChangeAtRuntime.HostBuilders
{
    /// <summary>
    /// Configures the Localization service
    /// </summary>
    public static class AddLocalizationHostBuilderExtension
    {
        /// <summary>
        /// Add the Json localization service
        /// </summary>
        /// <param name="hostBuilder">the application host</param>
        /// <returns>the configured host</returns>
        public static IHostBuilder AddJsonLocalization(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            hostBuilder.ConfigureServices(services =>
            {
                services.TryAddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
                services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

                services.PostConfigure<JsonLocalizationOptions>(options =>
                {
                    options.ResourcesPath = "Resources";
                });
            });
            return hostBuilder;
        }
    }
}
