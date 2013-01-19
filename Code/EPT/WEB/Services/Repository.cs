using EPT.DAL.DomainClasses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EPT.WEB.Services
{

    public class Repository 
    {


        private AdventureWorksService _adventureWorksService = new AdventureWorksService();
        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>a list of Employees</returns>
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _adventureWorksService.All<Employee>();
        }


        public IEnumerable<Customer> GetAllCustomers()
        {
            Thread.Sleep(500);
            return _adventureWorksService.All<Customer>();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await Task.Factory.StartNew(() => GetAllCustomers());
        }

        /// <summary>
        /// Gets the orders from customer.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns></returns>
        public IEnumerable<SalesOrderHeader> GetOrdersFromCustomer(int customerId)
        {
            return _adventureWorksService.GetList<SalesOrderHeader>(so => so.CustomerID == customerId);
        }


    }
}