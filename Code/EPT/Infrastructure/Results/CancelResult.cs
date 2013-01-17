using System;
using Caliburn.Micro;

namespace EPT.Infrastructure.Results
{
    internal class CancelResult : IResult
    {
        #region IResult Members

        public void Execute(ActionExecutionContext context)
        {
            Completed(this, new ResultCompletionEventArgs { WasCancelled = true });
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        #endregion
    }
}