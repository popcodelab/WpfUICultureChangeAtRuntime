using System;
using WpfUICultureChangeAtRuntime.ViewModels;

namespace WpfUICultureChangeAtRuntime.Navigators
{
    public enum ViewType
    {
        Files, Settings, Home
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
