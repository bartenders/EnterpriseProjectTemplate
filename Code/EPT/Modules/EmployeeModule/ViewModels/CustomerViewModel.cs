using Caliburn.Micro;
using EPT.DAL.Northwind;
using EPT.GUI.Helpers;
using EPT.Infrastructure.API;
using EPT.Modules.MasterDataModule.InternalMessages;
using System.Windows.Controls;

namespace EPT.Modules.MasterDataModule.ViewModels
{
    public class CustomerViewModel : Conductor<OrdersViewModel>.Collection.OneActive, IShellModule
    {
        private readonly Repository _repository;
        private readonly IEventAggregator _eventAggregator;

        public CustomerViewModel()
        {
            DisplayName = "Customers";
        }
        public CustomerViewModel(Repository repository, IEventAggregator eventAggregator)
            : this()
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
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

        private void InitializeData()
        {
            if (!Execute.InDesignMode)
            {
                Customers.AddRange(_repository.GetAllCustomers());
            }
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
            get { return 5; }
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
