using C969.Data;
using C969.Exceptions;
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
        /// <summary>
        /// Updates the customer information in the database
        /// </summary>
        /// <param name="workingCustomer">The updated Customer object</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
        public bool HandleUpdateCustomer(Customer workingCustomer)
        {
            if (workingCustomer.Equals(Customer))
            {
                return false;
            }
            try
            {
                var result = _customerData.UpdateCustomer(workingCustomer);
                if (result)
                {
                    int index = _customers.FindIndex(x => x.customerId == workingCustomer.customerId);
                    _customers[index] = workingCustomer;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates customer address in the database.
        /// </summary>
        /// <param name="workingCustomerAddress">The updated Address object.</param>
        /// <returns>True, if operation was successful. False, if not.</returns>
        public bool HandleUpdateAddress(Address workingCustomerAddress)
        {
            if (workingCustomerAddress.Equals(CustomerAddress))
            {
                return false;
            }
            try
            {
                var result = _addressData.UpdateAddress(workingCustomerAddress);
                return result;
            }
            catch (DuplicateData ex)
            {
                Address duplicateAddress = _addressData.GetAddressById(ex.DuplicateAddressId);
                var result = _addressData.UpdateAddress(duplicateAddress);
                return result;
            }
            catch
            {
                return false;
            }
        }
        public bool HandleUpdateCity(City workingCustomerCity)
        {
            if (workingCustomerCity.Equals(CustomerCity))
            {
                return false;
            }
            try
            {
                var result = _cityData.UpdateCity(workingCustomerCity);
                return result;
            } catch
            {
                return false;
            }
        }
        public List<Customer> Customers => _customers;
        public Customer Customer => _customerData.GetCustomerById(CustomerId);
        public Address CustomerAddress => _addressData.GetAddressById(Customer.addressId);
        public City CustomerCity => _cityData.GetCityById(CustomerAddress.cityId);
        public Country CustomerCountry => _countryData.GetCountryById(CustomerCity.countryId);
        public int CustomerId { get => _customerId; set => _customerId = value; }
    }
}
