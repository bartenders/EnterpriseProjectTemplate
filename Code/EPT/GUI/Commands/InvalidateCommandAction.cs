using System.Windows;
using System.Windows.Interactivity;

namespace EPT.GUI.Commands
{
    public class InvalidateCommandAction : TargetedTriggerAction<DependencyObject>
    {
        public InvalidateCommandAction()
        {
            
        }

        #region SwitchEmplyoeeViewCommand Dependency Property
        
    
        public DelegateCommandBase Command
        {
            get { return (DelegateCommandBase)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "SwitchEmplyoeeViewCommand",
                typeof (DelegateCommandBase),
                typeof (InvalidateCommandAction));

        #endregion //SwitchEmplyoeeViewCommand DependencyProperty
		

        protected override void Invoke(object parameter)
        {
            if (Command != null)
            {
                Command.Invalidate();
            }
        }
    }
}