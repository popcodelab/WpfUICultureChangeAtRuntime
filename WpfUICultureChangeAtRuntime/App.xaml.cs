using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Windows;
using WpfUICultureChangeAtRuntime.HostBuilders;

namespace WpfUICultureChangeAtRuntime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region private class members

        private readonly IHost _host;

        #endregion

        public App()
        {
            try
            {
                _host = CreateHostBuilder().Build();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"An error has occured when the application started " + Environment.NewLine + ex);
                Debug.WriteLine(@"Application closing ... ");
                Current.Shutdown();
            }
        }

        /// <summary>
        /// Creates and configures the .Net generic host which encapsulates the app's resources (Configuration, DI, Logging ...)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddJsonLocalization()
                .AddLogging()
                .AddServices()
                .AddViewModels()
                .AddViews();
        }

        /// <summary>
        /// Handles OnStartup event, better to avoid to use the OnStartup overriding , resources won't be loaded, so problems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // At that point, because we are in the event handler, we make sure that the window resources will be loaded
            _host.Start();

            Window window = _host.Services.GetRequiredService<MainWindow>();

            window.Show();
        }
    }
}
