using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using WpfUICultureChangeAtRuntime.Commands;
using WpfUICultureChangeAtRuntime.Navigators;
using WpfUICultureChangeAtRuntime.ViewModels.Factory;

namespace WpfUICultureChangeAtRuntime.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region "Private class members"

        private readonly ILogger _logger;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IConfiguration _configuration;
        private readonly INavigator _navigator;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        #endregion 


        #region "Properties"

        /// <summary>
        /// Current ViewModel
        /// </summary>
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        /// <summary>
        /// Navigation between views
        /// </summary>
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        #endregion "Properties"

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="configuration">Inject Configuration using DI</param>
        /// <param name="viewModelFactory">ViewModel factory</param>
        /// <param name="stringLocalizerFactory">StringLocalizer factory</param>
        /// <param name="loggerFactory">Inject Logger using DI</param>
        public MainViewModel(INavigator navigator, IConfiguration configuration, IViewModelFactory viewModelFactory, IStringLocalizerFactory stringLocalizerFactory, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MainViewModel>();
            _navigator = navigator;
            _configuration = configuration;
            _viewModelFactory = viewModelFactory;
            _navigator.StateChanged += Navigator_StateChanged;
            _stringLocalizerFactory = stringLocalizerFactory;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Home);
            LoadLanguage(_configuration?.GetValue<string>("DefaultCulture"));
            _logger.LogInformation("MainViewModel Created.");
        }


        private void Navigator_StateChanged()
        {
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;

            base.Dispose();
        }

        /// <summary>
        /// Feeds the language dictionary
        /// </summary>
        /// <param name="selectedCultureKey">The wanted culture</param>
        public void LoadLanguage(string selectedCultureKey)
        {
            try
            {
                _logger.LogInformation($"Change UI culture to {selectedCultureKey}.");
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(selectedCultureKey);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(selectedCultureKey);
                var localizer = _stringLocalizerFactory.Create(this.GetType());
                CommonViewModel.Current.CurrentLanguageDictionnary = localizer.GetAllStrings(false).ToDictionary(x => x.Name, x => x.Value);
                _logger.LogInformation($"UI culture has been changed to {selectedCultureKey}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(@"Impossible to find the culture files that your are using. UI will be affected." + Environment.NewLine + ex.Message);
            }
        }
    }
}
