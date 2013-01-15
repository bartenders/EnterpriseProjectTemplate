using Caliburn.Micro;
using EPT.Infrastructure.API;
using Ninject;
using System;

namespace EPT.Infrastructure.Framework
{
    public class ApplicationCloseCheck : IResult
    {
        readonly Action<IDialogManager, Action<bool>> _closeCheck;
        readonly IChild _screen;

        public ApplicationCloseCheck(IChild screen, Action<IDialogManager, Action<bool>> closeCheck)
        {
            this._screen = screen;
            this._closeCheck = closeCheck;
        }

        [Inject]
        public IShell Shell { get; set; }

        public void Execute(ActionExecutionContext context)
        {
            var documentWorkspace = _screen.Parent as IDocumentWorkspace;
            if (documentWorkspace != null)
                documentWorkspace.Edit(_screen);

            _closeCheck(Shell.Dialogs, result => Completed(this, new ResultCompletionEventArgs { WasCancelled = !result }));
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}