using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using WpfUICultureChangeAtRuntime.Navigators;
using WpfUICultureChangeAtRuntime.ViewModels;
using WpfUICultureChangeAtRuntime.ViewModels.Factory;

namespace WpfUICultureChangeAtRuntime.HostBuilders
{
    /// <summary>
    /// Add ViewModel
    /// </summary>
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            hostBuilder.ConfigureServices(services =>
            {

                services.AddTransient<MainViewModel>();
                services.AddTransient<FilesViewModel>();
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<HomeViewModel>();

                services.AddSingleton<CreateViewModel<FilesViewModel>>(services => () => services.GetRequiredService<FilesViewModel>());
                services.AddSingleton<CreateViewModel<SettingsViewModel>>(services => () => services.GetRequiredService<SettingsViewModel>());
                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton<CreateViewModel<MainViewModel>>(servicesCollection => () => CreateMainViewModel(servicesCollection));


            });

            return hostBuilder;
        }

        private static MainViewModel CreateMainViewModel(IServiceProvider services)
        {
            return new MainViewModel(services.GetRequiredService<INavigator>(), services.GetRequiredService<IConfiguration>(),
                services.GetRequiredService<IViewModelFactory>(),
                services.GetRequiredService<IStringLocalizerFactory>(),
                services.GetService<ILoggerFactory>());
        }
    }
}
