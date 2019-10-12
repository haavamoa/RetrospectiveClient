using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetrospectiveClient.Utils
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Func<object, Task> _command;

        public AsyncCommand(Func<object, Task> command, Func<object, bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute ?? (o => true);
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter = null)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            await _command(parameter);
        }
    }

    public abstract class AsyncCommandBase : IAsyncCommand, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract Task ExecuteAsync(object parameter = null);

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            // ISSUE: reference to a compiler-generated field
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged == null)
            {
                return;
            }
            // ISSUE: variable of the null type
            object local = null;
            var e = new EventArgs();
            canExecuteChanged(local, e);
        }
    }

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);

        void RaiseCanExecuteChanged();
    }
}