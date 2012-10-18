using System;
using System.Windows.Input;

namespace EPT.GUI.Commands
{
    partial class DelegateCommand : ICommand
    {
        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        #endregion
    }

    partial class DelegateCommand<T> : ICommand
    {
        [ThreadStatic]

        static readonly Lazy<bool> isReferenceOrNullableValueType =
            Lazy.Create(() => !typeof(T).IsValueType || typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>), false);

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null)
            {
                if (isReferenceOrNullableValueType.Value)
                {
                    return CanExecute((T)(object)null);
                }

                throw new InvalidCastException(string.Format("Cannot cast 'null' to {0}.",(typeof(T))));
            }

            return parameter is T && CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter == null)
            {
                if (isReferenceOrNullableValueType.Value)
                {
                    Execute((T)(object)null);
                }

                throw new InvalidCastException(string.Format("Cannot cast 'null' to {0}.", (typeof(T))));
            }

            Execute((T)parameter);
        }

        #endregion
    }
}