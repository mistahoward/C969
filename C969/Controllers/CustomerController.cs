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
        private readonly AddressData _addressData;
        private readonly CityData _cityData;
        private readonly CountryData _countryData;
        private readonly List<Customer> _customers;
        private int _customerId;
        public CustomerController()
        {
            _customerData = new CustomerData();
            _addressData = new AddressData();
            _cityData = new CityData();
            _countryData = new CountryData();

            _customers = new List<Customer>();

            _customers = _customerData.GetCustomers();
        }

        public List<Customer> Customers => _customers;
        public Customer Customer => _customerData.GetCustomerById(CustomerId);
        public Address CustomerAddress => _addressData.GetAddressById(Customer.addressId);
        public City CustomerCity => _cityData.GetCityById(CustomerAddress.cityId);
        public Country CustomerCountry => _countryData.GetCountryById(CustomerCity.countryId);
        public int CustomerId { get => _customerId; set => _customerId = value; }
    }
}
