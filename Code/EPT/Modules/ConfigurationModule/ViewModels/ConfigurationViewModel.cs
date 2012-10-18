using System.Windows.Controls;
using Caliburn.Micro;


namespace EP.Modules.ConfigurationModule.ViewModels
{
    public sealed class ConfigurationViewModel : Screen, IShellModule
    {
        public ConfigurationViewModel()
        {
            DisplayName = "Configuration Module";
            Text = "The content of this Textelement is definded in the ViewModel";
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
            get { return ImageHelper.CreateImage(UriHelper.MakePackUri<ConfigurationViewModel>(@"\Images\appbar.control.guide.png"), 48); }
        }

        public int OrderPriority
        {
            get { return 5; }
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
    }
}