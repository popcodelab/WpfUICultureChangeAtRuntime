using Microsoft.Extensions.Logging;
using System.Windows;

namespace WpfUICultureChangeAtRuntime
{

       /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region private class members

        private readonly ILogger _logger;

        #endregion


        public MainWindow(object dataContext, ILoggerFactory loggerFactory)
        {
            InitializeComponent();
            _logger = loggerFactory.CreateLogger<MainWindow>();
            DataContext = dataContext;
            _logger.LogInformation("InitializeComponent done.");
        }
    }
}
