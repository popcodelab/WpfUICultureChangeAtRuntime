using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WpfUICultureChangeAtRuntime.Navigators;

namespace WpfUICultureChangeAtRuntime.HostBuilders
{
    /// <summary>
    /// This extension adds the services needed by the application
    /// </summary>
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
            });

            return hostBuilder;
        }


    }
}
