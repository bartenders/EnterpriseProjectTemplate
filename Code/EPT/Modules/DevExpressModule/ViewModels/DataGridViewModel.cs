using System.Windows.Controls;
using EP.Infrastructure.Interfaces;

namespace EP.Modules.DevExpressModule.ViewModels
{
    public sealed class DataGridViewModel : Screen, IShellModule
    {
        private BindableCollection<ConfigurationSetting> _ConfigurationSettings;
        public BindableCollection<ConfigurationSetting> ConfigurationSettings
        {
            get { return _ConfigurationSettings ?? (_ConfigurationSettings = new BindableCollection<ConfigurationSetting>()); }
            set
            {
                if (value == _ConfigurationSettings) return;
                _ConfigurationSettings = value;
                NotifyOfPropertyChange(() => ConfigurationSettings);
            }
        }

        public DataGridViewModel()
        {
            DisplayName = "Devexpress Sample Module";
            _ConfigurationSettings = new BindableCollection<ConfigurationSetting>();
        }

        protected override void OnActivate()
        {
            //IsBusy = true;
            _ConfigurationSettings.Clear();
            using(var context = new Domain())
            {
                if (context.ConfigurationSettings.Any())
                {
                    _ConfigurationSettings.AddRange(context.ConfigurationSettings.ToList());    
                }
            }
            //IsBusy = false;
            base.OnActivate();
        }

        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.MakePackUri<DataGridViewModel>(@"Images\DevExpressSampleIcon.png"), 48); }
        }

        public int OrderPriority
        {
            get { return 10; }
        }
    }
}
