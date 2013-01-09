using EPT.DAL.Northwind;

namespace EPT.Modules.EmployeeModule.InternalMessages
{
    public class CustomerChangedMessage
    {
        private readonly Customer _customer;

        public CustomerChangedMessage(Customer customer)
        {
            _customer = customer;
        }

        public Customer Customer
        {
            get { return _customer; }
        }
    }
}