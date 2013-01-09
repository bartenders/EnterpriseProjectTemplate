using Caliburn.Micro;
using EPT.DAL.Northwind;
using EPT.Modules.EmployeeModule.InternalMessages;

namespace EPT.Modules.EmployeeModule.ViewModels
{
    public class CustomerViewModel : Screen
    {
        private readonly Repository _repository;
        private readonly IEventAggregator _eventAggregator;

        public CustomerViewModel()
        {
            
        }

        public CustomerViewModel(Repository repository, IEventAggregator eventAggregator)
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

        protected override void OnViewLoaded(object view)
        {
            Customers.AddRange(_repository.GetAllCustomers());
            base.OnViewLoaded(view);
        }
        
        protected override void OnActivate()
        {
            base.OnActivate();
        }

    }
}
