using Caliburn.Micro;
using EPT.Infrastructure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace EPT.Shell.ViewModels
{
    public class ShellModuleViewModel : Conductor<IShellModule>.Collection.OneActive
    {
        private readonly IEnumerable<IWindowCommand> _windowCommands;

        private bool IsDirty;

        public ShellModuleViewModel(IEnumerable<IShellModule> shellModules)
        {
            Items.AddRange(shellModules.OrderBy(x => x.OrderPriority));
        }

        protected override void OnInitialize()
        {
            ActivateItem(Items.FirstOrDefault());
            base.OnInitialize();
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(
                IsDirty || MessageBox.Show(
                    "Are you sure you want to cancel?  Changes will be lost.",
                    "Unsaved Changes",
                    MessageBoxButton.OKCancel
                    ) == MessageBoxResult.OK
                );
        }

        protected override void OnDeactivate(bool close)
        {
            // we dont want to lose our references
            base.OnDeactivate(false);
        }
    }
}