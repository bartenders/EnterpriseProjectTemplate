using System;
using Caliburn.Micro;

namespace EPT.Infrastructure.Framework
{
	public class SaveDataResult : IResult
	{
		public DomainContext Context { get; private set; }
		public SubmitOperation Result { get; private set; }

		public event EventHandler<ResultCompletionEventArgs> Completed;

		public SaveDataResult(DomainContext context)
		{
			Context = context;
		}

		public void Execute(ActionExecutionContext context)
		{
			Context.SubmitChanges(SaveDataCallback, null);
		}

		private void SaveDataCallback(SubmitOperation data)
		{
			Result = data;
			OnCompleted();
		}

		private void OnCompleted()
		{
			var handler = Completed;
			if (handler != null)
			{
				handler(this, new ResultCompletionEventArgs());
			}
		}
	}
}
