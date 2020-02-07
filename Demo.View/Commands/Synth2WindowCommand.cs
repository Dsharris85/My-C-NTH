using System;
using System.Windows.Input;

namespace Demo.View.Commands
{
    internal class Synth2WindowCommand : ICommand
    {
        public Synth2WindowCommand(MainViewModel vm)
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
            return ViewModel.CanOpenSynth2;
        }

        public void Execute(object parameter)
        {
            ViewModel.NewSynth2Window();
        }
    }
}
