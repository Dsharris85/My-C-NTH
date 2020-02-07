using System;
using System.Windows.Input;

namespace Demo.View.Commands
{
    internal class LoadCommand : ICommand
    {
        public LoadCommand(MainViewModel vm)
        {
            ViewModel = vm;
        }

        private MainViewModel ViewModel;

        // Add to wpf command system
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true; 
        }

        public void Execute(object parameter)
        {
            ViewModel.OpenFile();
        }
    }
}
