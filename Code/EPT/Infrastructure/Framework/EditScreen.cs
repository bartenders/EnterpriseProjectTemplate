using Caliburn.Micro;
using EPT.Infrastructure.API;
using Ninject;
using System;

namespace EPT.Infrastructure.Framework
{
    public class EditScreen : Screen, IHaveShutdownTask
    {
        bool _isDirty;

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                NotifyOfPropertyChange(() => IsDirty);
            }
        }

        [Inject]
        public IDialogManager Dialogs { get; set; }

        public override void CanClose(Action<bool> callback)
        {
            if (IsDirty)
                DoCloseCheck(Dialogs, callback);
            else callback(true);
        }

        public IResult GetShutdownTask()
        {
            return IsDirty ? new ApplicationCloseCheck(this, DoCloseCheck) : null;
        }

        protected virtual void DoCloseCheck(IDialogManager dialogs, Action<bool> callback)
        {
            dialogs.ShowMessageBox(
                "You have unsaved data. Are you sure you want to close this document? All changes will be lost.",
                "Unsaved Data",
                MessageBoxOptions.YesNo,
                box => callback(box.WasSelected(MessageBoxOptions.Yes))
                );
        }
    }
}