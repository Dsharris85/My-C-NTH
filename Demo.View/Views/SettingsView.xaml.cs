using Demo.View.ViewModels;
using System.Windows;

namespace Demo.View.Views
{
    public partial class SettingsView : Window
    {
        private SettingsViewModel ViewModel;

        public SettingsView(SettingsViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            DataContext = vm;
        }

    }
}
