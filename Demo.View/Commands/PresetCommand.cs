using System;
using System.Windows.Input;

namespace Demo.View.Commands
{
    internal class PresetCommand : ICommand
    {
        public PresetCommand(MainViewModel vm)
        {
            ViewModel = vm;
        }

        private MainViewModel ViewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.ResetPreset();
        }
    }
}
