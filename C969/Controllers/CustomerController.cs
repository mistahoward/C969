using C969.Data;
using C969.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Controllers
{
    public class CustomerController
    {
        private readonly CustomerData _customerData;
        private readonly List<Customer> _customers;
        private int _customerId;
        public CustomerController()
        {
            _customerData = new CustomerData();
            _customers = new List<Customer>();

            _customers = _customerData.GetCustomers();
        }

        public List<Customer> Customers => _customers;
        public Customer Customer => _customerData.GetCustomerById(CustomerId);
        public int CustomerId { get => _customerId; set => _customerId = value; }
    }
}
