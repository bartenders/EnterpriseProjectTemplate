using System;
using System.Diagnostics.Contracts;

namespace EPT.GUI.Commands
{
    /// <summary>
    /// Represents a parameterless command view model.
    /// </summary>
    public sealed partial class DelegateCommand : DelegateCommandBase
    {
        
        readonly Action execute;

        
        readonly Func<bool> canExecute;
        

        public DelegateCommand( Action execute,  Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand( Action execute)
            : this(execute, () => true)
        {

        }


        /// <summary>
        /// Determines whether this command can execute.
        /// </summary>
        /// <returns><c>true</c> if this instance can execute; otherwise, <c>false</c>.</returns>
        [Pure]
        public bool CanExecute()
        {
            return canExecute != null ? canExecute() : true;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            Contract.Requires(CanExecute());

            execute();
        }
    }

    /// <summary>
    /// Represents a parameterized command view model.
    /// </summary>
    /// <typeparam name="T">The command parameter type.</typeparam>
    public sealed partial class DelegateCommand<T> : DelegateCommandBase
    {
        
        readonly Action<T> execute;

        
        readonly Func<T, bool> canExecute;
       

        public DelegateCommand( Action<T> execute,  Func<T, bool> canExecute)
        {
            Contract.Requires(execute != null);

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand( Action<T> execute)
            : this(execute, (_) => true)
        {
        }

        /// <summary>
        /// Determines whether this command can execute.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns><c>true</c> if this instance can execute; otherwise, <c>false</c>.</returns>
        [Pure]
        public bool CanExecute(T argument)
        {
            return canExecute != null ? canExecute(argument) : true;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public void Execute(T argument)
        {
            Contract.Requires(CanExecute(argument));

            execute(argument);
        }
    }
}