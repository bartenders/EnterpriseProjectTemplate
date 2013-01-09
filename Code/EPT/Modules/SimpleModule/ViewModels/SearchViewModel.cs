using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using EPT.GUI.Helpers;
using EPT.Infrastructure.Interfaces;
using EPT.Infrastructure.Messages;
using Ninject;

namespace EPT.Modules.SearchModule.ViewModels
{
    public sealed class SearchViewModel : Conductor<IScreen>.Collection.OneActive, IShellModule, IHandle<EmployeeAddedMessage>
    {
        private string _SearchText;

		public SearchViewModel() {
			
		}
		
        public SearchViewModel(IEventAggregator aggregator)
        {
            aggregator.Subscribe(this);
            DisplayName = "Search Module";
        }

        public override void CanClose(System.Action<bool> callback)
        {
            base.CanClose(callback);
        }

        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.box.png"), 48);  }
        }

        
        
        public int OrderPriority
        {
            get { return 20; }
        }

        public bool ActiveMenuEntry
        {
            get { return true; }
        }

        public string SearchText
        {
            get { return _SearchText ?? (_SearchText = string.Empty); }
            set
            {
                if (value == _SearchText) return;
                _SearchText = value;
                NotifyOfPropertyChange(() => CanSearch);
                NotifyOfPropertyChange(() => SearchText);
            }
        }

      public void Search()
        {
            var searchResult = new TabItemViewModel
                {
                    DisplayName = string.Format("{0} ({1})", SearchText, Items.Count)
                };
            this.ActiveItem = searchResult;
        }

        public bool CanSearch
        {
            get { return !string.IsNullOrEmpty(SearchText); }
        }

        public void Handle(EmployeeAddedMessage message)
        {
            this.SearchText = message.MyMessage;
        }
    }
}