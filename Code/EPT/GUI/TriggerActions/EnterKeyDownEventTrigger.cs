using System;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EPT.GUI.TriggerActions
{
    /// <summary>
    /// Event Trigger invoked by Enter or Return Key Down 
    /// </summary>
    public class EnterKeyDownEventTrigger : EventTrigger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnterKeyDownEventTrigger"/> class.
        /// </summary>
        public EnterKeyDownEventTrigger()
            : base("KeyDown") {}

        protected override void OnEvent(EventArgs eventArgs)
        {
            var e = eventArgs as KeyEventArgs;
            if (e != null && (e.Key == Key.Enter || e.Key == Key.Return))
            {
                this.InvokeActions(eventArgs);
            }
        }
    }
}
