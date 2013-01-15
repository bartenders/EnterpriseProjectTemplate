using Caliburn.Micro;
using EPT.GUI.Helpers;
using EPT.Infrastructure.API;
using EPT.Infrastructure.Messages;
using EPT.Shell.Properties;
using System.Windows.Controls;
using Action = System.Action;

namespace EPT.Shell.ViewModels
{
    public class SettingsViewModel : Screen, IWindowCommand
    {
        private readonly IEventAggregator _eventAggregator;
        private ILog _log;


        public SettingsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DisplayName = "Settings";
            Settings.Default.PropertyChanged += Default_PropertyChanged;
            _log = LogManager.GetLog(typeof(SettingsViewModel));
        }

        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _log.Info("{0} changed to {1}", e.PropertyName, Settings.Default[e.PropertyName]);
            Settings.Default.Save();
        }

        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.settings.png"), 48); }
        }


        public string CommandDisplayName
        {
            get { return DisplayName; }
        }

        public void Save()
        {
            Settings.Default.Save();
        }

        /// <summary>
        /// Will be executed on the Command Click
        /// </summary>
        /// <value>
        /// The command action.
        /// </value>
        public Action CommandAction
        {
            get
            {
                return () => _eventAggregator.Publish(new ShowScreenMessage(this));
            }
        }

        public void NavigateBack()
        {
            _eventAggregator.Publish(new ShowScreenMessage(null));
        }
    }
}