using System.Collections.Generic;

namespace WpfUICultureChangeAtRuntime.ViewModels
{
    public class CommonViewModel : ViewModelBase
    {
        private static CommonViewModel _current;

        private Dictionary<string, string> _currentLanguageDictionnary;

        public Dictionary<string, string> CurrentLanguageDictionnary
        {
            get => _currentLanguageDictionnary;
            set => SetProperty(ref _currentLanguageDictionnary, value);
        }


        private CommonViewModel()
        {
            _currentLanguageDictionnary = null;
        }

        /// <summary>
        /// Gets the current instance of this singleton.
        /// </summary>
        /// <value>The current instance.</value>
        public static CommonViewModel Current => _current ??= new CommonViewModel();
    }
}
