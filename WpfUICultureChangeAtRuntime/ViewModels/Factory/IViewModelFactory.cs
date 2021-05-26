using WpfUICultureChangeAtRuntime.Navigators;

namespace WpfUICultureChangeAtRuntime.ViewModels.Factory
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
