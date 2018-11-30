﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TakeHomeChallenge.ViewModel
{
    public class Command : System.Windows.Input.ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        public Command(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new NullReferenceException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
