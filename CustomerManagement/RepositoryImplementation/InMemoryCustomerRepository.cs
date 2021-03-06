﻿using AbstractRepository;
using CustomerEntity;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryImplementation
{
    public class InMemoryCustomerRepository : CustomerRepository
    {
        List<Customer> customers;

        public InMemoryCustomerRepository()
        {
            customers = new List<Customer>();
        }

        public void AddCustomer(Customer cust)
        {
            customers.Add(cust);
        }

        public void DeleteAllCustomers()
        {
            customers.Clear();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customers;
        }

        public Customer GetCustomerById(string id)
        {
            return customers.FirstOrDefault(cust => cust.CustomerID == id);
        }

        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            return customers.Where(cust => cust.FullName.Contains(name));
        }
    }
}
