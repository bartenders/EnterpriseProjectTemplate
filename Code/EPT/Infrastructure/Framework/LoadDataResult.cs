using System;
using Caliburn.Micro;

namespace EPT.Infrastructure.Framework
{
public class LoadDataResult<TEntity> : IResult where TEntity : Entity
{
	public EntityQuery<TEntity> Query { get; set; }
	public DomainContext Context { get; set; }
	public LoadOperation<TEntity> Result { get; private set; }

	public event EventHandler<ResultCompletionEventArgs> Completed;

	public LoadDataResult(DomainContext context, EntityQuery<TEntity> query)
	{
		Query = query;
		Context = context;
	}

	public void Execute(ActionExecutionContext context)
	{
		Context.Load(Query, LoadDataCallback, null);
	}

	private void LoadDataCallback(LoadOperation<TEntity> data)
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
