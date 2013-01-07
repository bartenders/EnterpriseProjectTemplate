using System.Windows;
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
            
            if (Items.Count > 0)
            {
                var result = MessageBox.Show("Unsaved items, do you really want to navigate to a different screeen?",
                                   "unsaved changes", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                return;
            }
               
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

        private string _SearchText;
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
            _count++;

            var searchResult = new TabItemViewModel
                {
                    DisplayName = string.Format("{0} ({1})", SearchText, _count)
                };

            Items.Add(searchResult);

            ActivateItem(searchResult);
        }

        public bool CanSearch
        {
            get { return !string.IsNullOrEmpty(SearchText); }
        }
    }
}