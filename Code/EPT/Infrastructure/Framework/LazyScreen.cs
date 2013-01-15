using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace EPT.Infrastructure.Framework
{
	public static class LazyScreen
	{
		public static LazyScreen<TScreen, TMetadata> Create<TScreen, TMetadata>(ExportFactory<TScreen, TMetadata> factory)
		{
			return new LazyScreen<TScreen, TMetadata>(factory);
		}
	}

	public class LazyScreen<TScreen, TMetadata> : Screen
	{
		private readonly ExportFactory<TScreen, TMetadata> _factory;
		private TScreen _screen;
		private ExportLifetimeContext<TScreen> _export;
		private readonly object _lock = new object();

		public TMetadata Metadata
		{
			get
			{
				return _factory.Metadata;
			}
		}

		public bool IsScreenCreated
		{
			get
			{
				return _export != null;
			}
		}

		public TScreen Screen
		{
			get
			{
				lock (_lock)
				{
					if (!IsScreenCreated)
					{
						_export = _factory.CreateExport();
						_screen = _export.Value;

						SetScreenParent();
						if (IsActive)
						{
							ActivateScreen();
						}
					}
					return _screen;
				}
			}
		}

		public LazyScreen(ExportFactory<TScreen, TMetadata> factory)
		{
			_factory = factory;
		}

		public void Reset()
		{
			if (!IsScreenCreated)
			{
				return;
			}

			lock (_lock)
			{
				DeactivateScreen(true);

				_export.Dispose();
				_export = null;
				_screen = default(TScreen);
			}
			NotifyOfPropertyChange(() => IsScreenCreated);
			NotifyOfPropertyChange(() => Screen);
		}

		protected override void OnActivate()
		{
			ActivateScreen();
		}

		protected override void OnDeactivate(bool close)
		{
			DeactivateScreen(close);
		}

		public override void CanClose(Action<bool> callback)
		{
			var closableScreen = _screen as IGuardClose;
			if (closableScreen != null)
			{
				closableScreen.CanClose(callback);
			}
			else
			{
				base.CanClose(callback);
			}
		}

		public new void TryClose()
		{
			var closableScreen = _screen as IClose;
			if (closableScreen != null)
			{
				closableScreen.TryClose();
			}
			base.TryClose();
		}

		private void SetScreenParent()
		{
			var childScreen = _screen as IChild;
			if (childScreen != null)
			{
				childScreen.Parent = this.Parent;
			}
		}

		public override object Parent
		{
			get
			{
				return base.Parent;
			}
			set
			{
				base.Parent = value;

				if (IsScreenCreated)
				{
					SetScreenParent();
				}
			}
		}

		private void ActivateScreen()
		{
			var activatableScreen = _screen as IActivate;
			if (activatableScreen != null)
			{
				activatableScreen.Activate();
			}
		}

		private void DeactivateScreen(bool close)
		{
			var deactivatableScreen = _screen as IDeactivate;
			if (deactivatableScreen != null)
			{
				deactivatableScreen.Deactivate(close);
			}
		}
	}
}
