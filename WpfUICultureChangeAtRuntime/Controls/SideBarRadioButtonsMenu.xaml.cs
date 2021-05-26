using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUICultureChangeAtRuntime.Controls
{
    /// <summary>
    /// Interaction logic for SideBarRadioButtonsMenu.xaml
    /// </summary>
    public partial class SideBarRadioButtonsMenu : UserControl
    {

        #region DependencyProperty

        public static DependencyProperty UpdateCurrentViewModelCommandProperty = DependencyProperty.Register("UpdateCurrentViewModelCommand",
            typeof(ICommand), typeof(SideBarRadioButtonsMenu), new PropertyMetadata(null));

        public ICommand UpdateCurrentViewModelCommand
        {
            get => (ICommand)GetValue(UpdateCurrentViewModelCommandProperty);
            set => SetValue(UpdateCurrentViewModelCommandProperty, value);
        }

        #endregion

        public SideBarRadioButtonsMenu()
        {
            InitializeComponent();

        }
    }
}
