using System;

namespace ETP.DAL.Core
{
    public class OperationResult
    {
        public bool Status { get; set; }
        public int RecordsAffected { get; set; }
        public string Message { get; set; }
        public Object OperationId { get; set; }
        public OperationResultType ResultType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }

        public static OperationResult CreateFromException(string message, Exception ex)
        {
            var operationResult = new OperationResult
            {
                Status = false,
                Message = message,
                OperationId = null
            };

            if (ex != null)
            {
                operationResult.ExceptionMessage = ex.Message;
                operationResult.ExceptionStackTrace = ex.StackTrace;
                operationResult.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                operationResult.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return operationResult;
        }

    }
}
