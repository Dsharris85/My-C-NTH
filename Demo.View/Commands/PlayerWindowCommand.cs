using System;
using System.Windows.Input;
using Demo.View.ViewModels;

namespace Demo.View.Commands
{
    internal class PlayerWindowCommand : ICommand
    {
        public PlayerWindowCommand(MainViewModel vm)
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
            return ViewModel.CanOpenPlayer;
        }

        public void Execute(object parameter)
        {
            ViewModel.NewPlayerWindow();
        }
    }
}
