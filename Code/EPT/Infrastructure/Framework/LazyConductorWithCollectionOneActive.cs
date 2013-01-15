using System;
using System.Linq;
using Caliburn.Micro;

namespace EPT.Infrastructure.Framework
{
	public partial class LazyConductor<TScreen, TMetadata>
	{
		public partial class Collection
		{
			public class OneActive : Conductor<LazyScreen<TScreen, TMetadata>>.Collection.OneActive
			{
				public override void DeactivateItem(LazyScreen<TScreen, TMetadata> item, bool close)
				{
					if (item == null)
					{
						return;
					}

					if (close)
					{
						CloseStrategy.Execute(new[] { item }, (canClose, closable) =>
							{
								if (canClose)
								{
									CloseItemCore(item);
								}
							});
					}
					else
					{
						ScreenExtensions.TryDeactivate(item, false);
					}
				}

				public override void CanClose(Action<bool> callback)
				{
					var openedItems = Items.Where(x => x.IsScreenCreated);
					CloseStrategy.Execute(openedItems, (canClose, closable) =>
						{
							closable.Apply(CloseItemCore);
							callback(canClose);
						});
				}

				protected override LazyScreen<TScreen, TMetadata> EnsureItem(LazyScreen<TScreen, TMetadata> newItem)
				{
					var node = newItem as IChild;
					if (node != null && node.Parent != this)
					{
						node.Parent = this;
					}

					return newItem;
				}

				private void CloseItemCore(LazyScreen<TScreen, TMetadata> item)
				{
					if (item.Equals(ActiveItem))
					{
						var next = DetermineNextItemToActivate(item);
						ChangeActiveItem(next, true);
					}
					else
					{
						ScreenExtensions.TryDeactivate(item, true);
					}

					item.Reset();
				}

				protected LazyScreen<TScreen, TMetadata> DetermineNextItemToActivate(LazyScreen<TScreen, TMetadata> currentItem)
				{
					var next = Items.FirstOrDefault(x => x != currentItem && x.IsScreenCreated);
					return next;
				}
			}
		}
	}
}
