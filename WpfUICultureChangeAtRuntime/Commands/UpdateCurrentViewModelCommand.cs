using System;
using WpfUICultureChangeAtRuntime.Navigators;
using WpfUICultureChangeAtRuntime.ViewModels.Factory;

namespace WpfUICultureChangeAtRuntime.Commands
{
    public class UpdateCurrentViewModelCommand : CommandBase
    {

        private readonly IViewModelFactory _viewModelFactory;
        private INavigator _navigator;

        

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public override bool CanExecute(object parameter)
        {
            // Not that it returns true in the base class (ICommand)
            return true;
        }

        public override void Execute(object parameter)
        {
            if (!(parameter is ViewType)) return;
            var viewType = (ViewType)parameter;
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }

    }
}
