using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace WpfUICultureChangeAtRuntime.ViewModels
{
    public class SettingsViewModel: ViewModelBase
    {
        #region Private class members

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        private MainViewModel _mainViewModel;

        private string _selectedLanguage;

        #endregion Private class members

        #region Properties

        public Dictionary<string, string> Languages { get; set; }


        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty<string>(ref _selectedLanguage, value);
        }

        #endregion

        #region Command

        public ICommand SelectionChangedCommand => new ActionCommand(CultureComboBoxSelectionChanged);

        private void CultureComboBoxSelectionChanged(object parameter)
        {
            if (parameter == null) return;
            var selectedCultureKey = ((KeyValuePair<string, string>)parameter).Key;
            _mainViewModel.LoadLanguage(selectedCultureKey);
        }

        #endregion


        public SettingsViewModel(IConfiguration configuration, MainViewModel mainViewModel, ILoggerFactory loggerFactory)
        {
            _mainViewModel = mainViewModel;
            _logger = loggerFactory.CreateLogger<SettingsViewModel>();
            _logger.LogInformation("Creating a SettingsViewModel .... ");
            _configuration = configuration;
            SelectedLanguage = _configuration.GetValue<string>("DefaultCulture");
            Languages = configuration.GetSection("SupportedLanguages").Get<Dictionary<string, string>>();
            _logger.LogInformation($"Found {Languages.Count} supported languages.");
        }
    }
}
