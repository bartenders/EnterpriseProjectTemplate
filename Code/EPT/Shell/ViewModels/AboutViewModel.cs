using Caliburn.Micro;
using EPT.GUI.Helpers;
using EPT.Infrastructure.API;
using EPT.Infrastructure.Messages;
using System.Windows.Controls;
using Action = System.Action;

namespace EPT.Shell.ViewModels
{
    public class AboutViewModel : Screen, IWindowCommand
    {
        private readonly IEventAggregator _eventAggregator;

        public AboutViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DisplayName = "About";
        }

        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.information.circle.png"), 48); }
        }

        public string CommandDisplayName
        {
            get { return DisplayName; }
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