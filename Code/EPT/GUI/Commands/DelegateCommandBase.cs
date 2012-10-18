using System;
using System.Diagnostics.Contracts;

namespace EPT.GUI.Commands
{
    /// <summary>
    /// Represents a common functionality of command view models.
    /// </summary>
    public abstract class DelegateCommandBase
    {
        internal DelegateCommandBase() {}

        /// <summary>
        /// Invalidates this instance and forces the UI to requery the <see cref="ICommand.CanExecute"/> method.
        /// </summary>
        public void Invalidate()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        void OnCanExecuteChanged(EventArgs args)
        {
            Contract.Requires(args != null);

            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, args);
            }
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}