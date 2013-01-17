using Caliburn.Micro;
using EPT.Infrastructure.API;
using EPT.Infrastructure.Results;
using System;

namespace EPT.Infrastructure.Framework
{
    public class EditScreen : Screen, IHaveShutdownTask
    {
        private readonly IDialogManager _dialogManager;
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

        public EditScreen(IDialogManager dialogManager)
        {
            _dialogManager = dialogManager;
        }


        public override void CanClose(Action<bool> callback)
        {
            if (IsDirty)
                DoCloseCheck(callback);
            else callback(true);
        }

        public IResult GetShutdownTask()
        {
            return IsDirty ? new ApplicationCloseCheck(this, DoCloseCheck) : null;
        }

        protected virtual void DoCloseCheck(Action<bool> callback)
        {
            _dialogManager.ShowMessageBox(
                "You have unsaved data. Are you sure you want to close this document? All changes will be lost.",
                "Unsaved Data"
                );
        }
    }

    public interface IDialogManager
    {
        bool ShowMessageBox(string title, string message);
    }
}