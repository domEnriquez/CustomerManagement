using System;
using System.Collections.Generic;

namespace CustomerManagementConsole
{
    public interface CustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(string id);
        IEnumerable<Customer> GetCustomersByName(string name);
        void AddCustomer(Customer cust);
        void DeleteAllCustomers();
    }
}
