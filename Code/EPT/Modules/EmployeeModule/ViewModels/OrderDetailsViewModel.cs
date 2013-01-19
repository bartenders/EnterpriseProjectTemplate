using Caliburn.Micro;
using EPT.DAL.DomainClasses;

namespace EPT.Modules.MasterDataModule.ViewModels
{
    public class OrderDetailsViewModel : Screen
    {
        private readonly SalesOrderHeader _selectedOrder;
        private readonly IWindowManager _windowManager;

        public OrderDetailsViewModel(SalesOrderHeader selectedOrder, IWindowManager windowManager)
        {
            _selectedOrder = selectedOrder;
            _windowManager = windowManager;
            //_selectedOrder.PropertyChanged += (sender, args) =>
            //    {
            //        IsDirty = true;
            //    };
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (value == _isDirty) return;
                _isDirty = value;
                NotifyOfPropertyChange(() => IsDirty);
            }
        }


        public SalesOrderHeader Order
        {
            get { return _selectedOrder; }
        }

    }
}
