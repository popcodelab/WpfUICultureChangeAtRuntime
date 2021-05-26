using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WpfUICultureChangeAtRuntime.HostBuilders
{
    /// <summary>
    /// Add logging service
    /// </summary>
    public static class AddLoggingHostBuilderExtensions
    {
        public static IHostBuilder AddLogging(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            hostBuilder.ConfigureServices(services =>
            {
                services.AddLogging(config =>
                {
                    config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
                    config.AddConsole(); // Log to console
                });
            });

            return hostBuilder;
        }
    }
}
