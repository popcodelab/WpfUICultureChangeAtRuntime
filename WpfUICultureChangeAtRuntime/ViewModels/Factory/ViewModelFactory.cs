using System;
using WpfUICultureChangeAtRuntime.Navigators;

namespace WpfUICultureChangeAtRuntime.ViewModels.Factory
{
    public class ViewModelFactory : IViewModelFactory
    {
        #region Private class members

        private readonly CreateViewModel<FilesViewModel> _createFilesViewModel;
        private readonly CreateViewModel<SettingsViewModel> _createSettingsViewModel;
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createFilesViewModel"></param>
        /// <param name="createSettingsViewModel"></param>
        /// <param name="createHomeViewModel"></param>
        public ViewModelFactory(CreateViewModel<FilesViewModel> createFilesViewModel,
            CreateViewModel<SettingsViewModel> createSettingsViewModel,
            CreateViewModel<HomeViewModel> createHomeViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createFilesViewModel = createFilesViewModel;
            _createSettingsViewModel = createSettingsViewModel;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewType">ViewModel type</param>
        /// <returns>the created VM</returns>
        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Files:
                    return _createFilesViewModel();
                case ViewType.Settings:
                    return _createSettingsViewModel();
                case ViewType.Home:
                    return _createHomeViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }


    }
}
