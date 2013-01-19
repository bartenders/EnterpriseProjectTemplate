using Caliburn.Micro;
using EPT.DAL.DomainClasses;
using EPT.Infrastructure.API;
using EPT.Modules.MasterDataModule.InternalMessages;
using System.Threading.Tasks;
using EPT.WEB.Services;

namespace EPT.Modules.MasterDataModule.ViewModels
{
    public class OrdersViewModel : Screen, IHandle<CustomerChangedMessage>
    {
        private readonly Repository _repository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBusyWatcher _busyWatcher;
        private readonly IWindowManager _windowManager;

        public OrdersViewModel()
        {

        }

        public OrdersViewModel(Repository repository, IEventAggregator eventAggregator, IBusyWatcher busyWatcher, IWindowManager windowManager)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _busyWatcher = busyWatcher;
            _windowManager = windowManager;
            _eventAggregator.Subscribe(this);
        }

        public IBusyWatcher BusyWatcher { get { return _busyWatcher; } }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        private BindableCollection<SalesOrderHeader> _orders;
        public BindableCollection<SalesOrderHeader> Orders
        {
            get { return _orders ?? (_orders = new BindableCollection<SalesOrderHeader>()); }
            set
            {
                if (value == _orders) return;
                _orders = value;
                NotifyOfPropertyChange(() => Orders);
            }
        }

        private SalesOrderHeader _selectedOrder;
        public SalesOrderHeader SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (value == _selectedOrder) return;
                _selectedOrder = value;
                NotifyOfPropertyChange(() => SelectedOrder);
                NotifyOfPropertyChange(() => CanEditOrder);
            }
        }

        public void EditOrder()
        {
            _windowManager.ShowWindow(new OrderDetailsViewModel(SelectedOrder, _windowManager));
        }

        public bool CanEditOrder
        {
            get { return SelectedOrder != null; }
        }


        public void Handle(CustomerChangedMessage message)
        {
            Orders.Clear();

            if (message.Customer != null && message.Customer.CustomerID > 0)
            {
                var ticket = BusyWatcher.GetTicket();
                Task.Factory.StartNew(() => Orders.AddRange(_repository.GetOrdersFromCustomer(message.Customer.CustomerID))).ContinueWith((x) => ticket.Dispose());
                OrderDetails = string.Format("Order Details for customer {0}", message.Customer.AccountNumber);
            }
        }

        private string _orderDetails;
        public string OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                if (value == _orderDetails) return;
                _orderDetails = value;
                NotifyOfPropertyChange(() => OrderDetails);
            }
        }

    }

}
