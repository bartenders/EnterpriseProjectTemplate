using EPT.DAL.DomainClasses;

namespace EPT.Modules.MasterDataModule.InternalMessages
{
    public class CustomerChangedMessage
    {
        private readonly Customer _customer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerChangedMessage" /> class.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public CustomerChangedMessage(Customer customer)
        {
            _customer = customer;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public Customer Customer
        {
            get { return _customer; }
        }
    }
}