﻿using System;
using Caliburn.Micro;

namespace EPT.Infrastructure.Results
{
    internal class CompleteWithErrorResult : IResult
    {
        private readonly Exception _error;

        public CompleteWithErrorResult(Exception error)
        {
            _error = error;
        }

        #region IResult Members

        public void Execute(ActionExecutionContext context)
        {
            Completed(this, new ResultCompletionEventArgs { Error = _error });
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        #endregion
    }
}