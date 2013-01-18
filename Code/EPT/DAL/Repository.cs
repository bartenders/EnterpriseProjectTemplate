using EPT.Infrastructure.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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


        public IEnumerable<Customer> GetAllCustomers()
        {
            Thread.Sleep(500);
            using (var context = new NorthwindEntities(GetEntityConnection()))
            {
                return (from item in context.Customers
                        select item).ToList();
            }
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
        public IEnumerable<Order> GetOrdersFromCustomer(string customerId)
        {
            var orders = new List<Order>();
            using (var context = new NorthwindEntities(GetEntityConnection()))
            {
                context.ContextOptions.LazyLoadingEnabled = false;

                orders = (from item in context.Orders
                          where item.CustomerID.Equals(customerId)
                          select item).ToList();
            }
            return orders;
        }

        private static EntityConnection GetEntityConnection()
        {
            return Connection.CreateConnectionString(typeof(Repository).Assembly, "Northwind", GetConnectionString());
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;
        }
    }
}