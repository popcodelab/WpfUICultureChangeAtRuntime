using System;
using WpfUICultureChangeAtRuntime.ViewModels;

namespace WpfUICultureChangeAtRuntime.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }
        public event Action StateChanged;





    }
}
