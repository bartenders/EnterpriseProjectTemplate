using Caliburn.Micro;
using EPT.GUI.Helpers;
using EPT.Infrastructure.API;
using System.Windows.Controls;

namespace EPT.Modules.SearchModule.ViewModels
{
    public sealed class SearchViewModel : Conductor<IScreen>.Collection.OneActive, IShellModule
    {
        private string _searchText;
        private ILog _log;

        public SearchViewModel()
        {
            DisplayName = "Search Module";
        }

        public SearchViewModel(IEventAggregator aggregator)
            : this()
        {
            aggregator.Subscribe(this);
            _log = LogManager.GetLog(typeof(IShellModule));
        }

        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.box.png"), 48); }
        }

        public int OrderPriority
        {
            get { return 5; }
        }

        public bool ActiveMenuEntry
        {
            get { return true; }
        }

        public string SearchText
        {
            get { return _searchText ?? (_searchText = string.Empty); }
            set
            {
                if (value == _searchText) return;
                _searchText = value;
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
    }
}