using Caliburn.Micro;
using EPT.DAL.Northwind;
using EPT.Modules.EmployeeModule.InternalMessages;

namespace EPT.Modules.EmployeeModule.ViewModels
{
    public class OrdersViewModel : Screen, IHandle<CustomerChangedMessage>
    {
        private readonly Repository _repository;
        private readonly IEventAggregator _eventAggregator;

        public OrdersViewModel()
        {
            
        }

        public OrdersViewModel(Repository repository, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        private BindableCollection<Order> _orders;
        public BindableCollection<Order> Orders
        {
            get { return _orders ?? (_orders = new BindableCollection<Order>()); }
            set
            {
                if (value == _orders) return;
                _orders = value;
                NotifyOfPropertyChange(() => Orders);
            }
        }

        public void Handle(CustomerChangedMessage message)
        {
            Orders.AddRange(_repository.GetOrdersFromCustomer(message.Customer.CustomerID));
        }
    }

}
