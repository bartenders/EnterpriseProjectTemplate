using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using EPT.Infrastructure.Data;

namespace EPT.DAL.Northwind
{
    //Todo: Implement Repository .NET 4.5 Async Friendly
    public class Repository
    {
        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>a list of Employees</returns>
        public IEnumerable<Employee> GetAllEmployees()
        {
            using (var context = new NorthwindEntities(GetEntityConnection()))
            {
                return (from item in context.Employees
                        select item).ToList();
            }
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = new NorthwindEntities(GetEntityConnection()))
            {
                return (from item in context.Customers
                        select item).ToList();
            }
        }

        /// <summary>
        /// Gets the orders from customer.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrdersFromCustomer(string customerId)
        {
            using (var context = new NorthwindEntities(GetEntityConnection()))
            {
                return (from item in context.Orders
                        where item.CustomerID.Equals(customerId)
                        select item).ToList();
            }
        }

        private static EntityConnection GetEntityConnection()
        {
            return Connection.CreateConnection(typeof(Repository).Assembly, "Northwind", GetConnectionString());
        }

        //TODO: Replace with configuration logic
        private static string GetConnectionString()
        {
            return "Data Source=.;Initial Catalog=Northwind;Integrated Security=SSPI;MultipleActiveResultSets=True;";
        }
    }
}