using Caliburn.Micro;
using EPT.DAL.Northwind;
using EPT.GUI.Helpers;
using EPT.Infrastructure.API;
using EPT.Modules.MasterDataModule.InternalMessages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EPT.Modules.MasterDataModule.ViewModels
{
    public class CustomersViewModel : Conductor<OrdersViewModel>.Collection.OneActive, IShellModule
    {
        private readonly Repository _repository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBusyWatcher _busyWatcher;
        private readonly IWindowManager _windowManager;
        private IEnumerable<Customer> _refCustomers;

        public CustomersViewModel()
        {
            DisplayName = "Customers";
        }
        public CustomersViewModel(Repository repository, IEventAggregator eventAggregator, IBusyWatcher busyWatcher, IWindowManager windowManager)
            : this()
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _busyWatcher = busyWatcher;
            _windowManager = windowManager;
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (value == _selectedCustomer) return;
                _selectedCustomer = value;
                //TODO Create a order factory?
                if (value != null)
                {
                    ActiveItem = IoC.Get<OrdersViewModel>();
                }
                //TODO the customer should be injected by ninject factory instead of eventaggregator
                _eventAggregator.Publish(new CustomerChangedMessage(value));
                NotifyOfPropertyChange(() => SelectedCustomer);
                NotifyOfPropertyChange(() => CanSearch);
                NotifyOfPropertyChange(() => CanEditCustomer);
            }
        }

        public void Search()
        {
            var busyTicket = _busyWatcher.GetTicket();
            Task.Factory.StartNew(() => _refCustomers.Where(x => x.ContactName.Contains(this.CustomerSearchText)).ToList())
            .ContinueWith((x) =>
            {
                Customers = new BindableCollection<Customer>(x.Result);
                busyTicket.Dispose();
            });
        }

        public bool CanSearch
        {
            get
            {
                return (_refCustomers != null) && (_customerSearchText.Length > 2);
            }
        }

        private string _customerSearchText;
        public string CustomerSearchText
        {
            get { return _customerSearchText ?? (_customerSearchText = string.Empty); }
            set
            {
                if (value == _customerSearchText) return;
                _customerSearchText = value;
                NotifyOfPropertyChange(() => CustomerSearchText);
                NotifyOfPropertyChange(() => CanSearch);
            }
        }

        private BindableCollection<Customer> _customers;
        public BindableCollection<Customer> Customers
        {
            get { return _customers ?? (_customers = new BindableCollection<Customer>()); }
            set
            {
                if (value == _customers) return;
                _customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }

        public IBusyWatcher BusyWatcher
        {
            get { return _busyWatcher; }
        }

        private async void InitializeDataAsync()
        {
            if (Execute.InDesignMode) return;
            using (_busyWatcher.GetTicket())
            {
                var customers = await _repository.GetAllCustomersAsync();
                Customers.AddRange(customers);
                _refCustomers = Customers;
                NotifyOfPropertyChange(() => CanSearch);
                NotifyOfPropertyChange(() => CanEditCustomer);
            }
        }

        private void InitializeData()
        {
            if (Execute.InDesignMode) return;
            var ticket = _busyWatcher.GetTicket();

            Task.Factory.StartNew(() =>
                {
                    return _repository.GetAllCustomers();
                }).ContinueWith((task) =>
                    {
                        Customers.AddRange(task.Result);
                        _refCustomers = Customers;
                        NotifyOfPropertyChange(() => CanSearch);
                        NotifyOfPropertyChange(() => CanEditCustomer);
                        ticket.Dispose();
                    });

        }

        public void EditCustomer()
        {
            _windowManager.ShowDialog(this, "DetailsView");
        }

        public bool CanEditCustomer
        {
            get { return SelectedCustomer != null; }
        }

        protected override void OnActivate()
        {
            InitializeData();

            base.OnActivate();
        }

        /// <summary>
        /// Gets the icon image.
        /// </summary>
        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"Images\Light\appbar.user.png"), 48); }
        }

        /// <summary>
        /// Gets the order priority.
        /// </summary>
        public int OrderPriority
        {
            get { return 15; }
        }

        /// <summary>
        /// Gets a value indicating whether to Show this Module in the Menu Entry
        /// </summary>
        /// <value>
        ///   <c>true</c> if [active menu entry]; otherwise, <c>false</c>.
        /// </value>
        public bool ActiveMenuEntry
        {
            get { return true; }
        }
    }
}
