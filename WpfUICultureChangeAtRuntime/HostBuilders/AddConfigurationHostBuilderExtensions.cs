using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace WpfUICultureChangeAtRuntime.HostBuilders
{
    /// <summary>
    /// This extension loads the configuration from appsettings.json
    /// </summary>
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            hostBuilder.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                c.AddEnvironmentVariables();
            });

            return hostBuilder;
        }
    }
}
