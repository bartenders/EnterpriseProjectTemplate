using System.Windows.Controls;
using Caliburn.Micro;
using EPT.GUI.Helpers;
using EPT.Infrastructure.Interfaces;

namespace EPT.Modules.SimpleModule.ViewModels
{
    public sealed class SimpleViewModel : Conductor<IScreen>.Collection.OneActive, IShellModule
    {
        int _count = 1;

        public SimpleViewModel()
        {
            DisplayName = "Simple Module";
            Text = "This is a databound Textelement";
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }

        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.box.png"), 48);  }
        }

        public int OrderPriority
        {
            get { return 20; }
        }

        private string _Text;
        public string Text
        {
            get { return _Text ?? (_Text = string.Empty); }
            set
            {
                if (value == _Text) return;
                _Text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public void OpenTab()
        {
            ActivateItem(new TabItemViewModel
            {
                DisplayName = "Tab " + _count++
            });
        }
    }
}