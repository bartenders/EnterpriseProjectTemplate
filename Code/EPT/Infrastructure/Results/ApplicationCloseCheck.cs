using Caliburn.Micro;
using EPT.Infrastructure.API;
using System;

namespace EPT.Infrastructure.Results
{
    public class ApplicationCloseCheck : IResult
    {
        readonly Action<Action<bool>> closeCheck;
        readonly IChild screen;

        public ApplicationCloseCheck(IChild screen, Action<Action<bool>> closeCheck)
        {
            this.screen = screen;
            this.closeCheck = closeCheck;
        }

        public void Execute(ActionExecutionContext context)
        {
            var documentWorkspace = screen.Parent as IDocumentWorkspace;
            if (documentWorkspace != null)
                documentWorkspace.Edit(screen);

            closeCheck(result => Completed(this, new ResultCompletionEventArgs { WasCancelled = !result }));
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}