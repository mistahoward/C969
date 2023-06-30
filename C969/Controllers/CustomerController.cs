using C969.Data;
using C969.Exceptions;
using C969.Models;
using C969.Utilities;
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
        /// <exception cref="InvalidObject">Thrown if workingCustomer is incomplete</exception>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
        public bool HandleUpdateCustomer(Customer workingCustomer)
        {
            if (workingCustomer.Equals(Customer))
            {
                return false;
            }
            var validCustomer = ModelValidator.ValidateModel(workingCustomer);
            if (!validCustomer)
            {
                throw new InvalidObject("Customer is missing required data");
            }
            try
            {
                workingCustomer.UpdateCustomer();
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
        /// <exception cref="InvalidObject">Thrown if workingCustomerAddress is incomplete</exception>
        /// <returns>True, if operation was successful. False, if not.</returns>
        public bool HandleUpdateAddress(Address workingCustomerAddress)
        {
            if (workingCustomerAddress.Equals(CustomerAddress))
            {
                return false;
            }
            var validAddress = ModelValidator.ValidateModel(workingCustomerAddress);
            if (!validAddress)
            {
                throw new InvalidObject("Address is missing required data");
            }    
            try
            {
                workingCustomerAddress.UpdateAddress();
                var result = _addressData.UpdateAddress(workingCustomerAddress);
                return result;
            }
            catch (DuplicateData ex)
            {
                Address duplicateAddress = _addressData.GetAddressById(ex.DuplicateId);
                Customer updatedCustomer = Customer;
                updatedCustomer.addressId = duplicateAddress.addressId;
                var result = _customerData.UpdateCustomer(updatedCustomer);
                return result;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the city of a customer in the database
        /// </summary>
        /// <param name="workingCustomerCity">The updated City object</param>
        /// <exception cref="InvalidObject">Thrown if workingCustomerCity is incomplete</exception>
        /// <returns>True, if operation was successful. False, if not</returns>
        public bool HandleUpdateCity(City workingCustomerCity)
        {
            if (workingCustomerCity.Equals(CustomerCity))
            {
                return false;
            }
            var validCity = ModelValidator.ValidateModel(workingCustomerCity);
            if (!validCity)
            {
                throw new InvalidObject("City is missing required data");
            }
            try
            {
                workingCustomerCity.UpdateCity();
                var result = _cityData.UpdateCity(workingCustomerCity);
                return result;
            }
            catch (DuplicateData ex)
            {
                City duplicateCity = _cityData.GetCityById(ex.DuplicateId);
                Address updatedAddress = CustomerAddress;
                updatedAddress.cityId = duplicateCity.cityId;
                var result = _addressData.UpdateAddress(updatedAddress);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Updates the country of a customer in the database
        /// </summary>
        /// <param name="workingCustomerCountry">The updated Country object</param>
        /// <exception cref="InvalidObject">Thrown if workingCustomerCountry is incomplete</exception>
        /// <returns>True, if operation was successful. False, if not.</returns>
        public bool HandleUpdateCountry(Country workingCustomerCountry)
        {
            if (workingCustomerCountry.Equals(CustomerCountry))
            {
                return false;
            }
            var validCountry = ModelValidator.ValidateModel(workingCustomerCountry);
            if (!validCountry)
            {
                throw new InvalidObject("Country is missing required data");
            }
            try
            {
                workingCustomerCountry.UpdateCountry();    
                var result = _countryData.UpdateCountry(workingCustomerCountry);
                return result;
            }
            catch (DuplicateData ex)
            {
                Country duplicateCountry = _countryData.GetCountryById(ex.DuplicateId);
                City updatedCity = CustomerCity;
                updatedCity.countryId = duplicateCountry.countryId;
                var result = _cityData.UpdateCity(updatedCity);
                return result;
            }
            catch
            {
                return false;
            }
        }
        public List<Customer> Customers => _customers;
        public Customer Customer => CustomerId == 0 ? new Customer() : _customerData.GetCustomerById(CustomerId);
        public Address CustomerAddress => Customer.addressId == 0 ? new Address() : _addressData.GetAddressById(Customer.addressId);
        public City CustomerCity => CustomerAddress.cityId == 0 ? new City() : _cityData.GetCityById(CustomerAddress.cityId);
        public Country CustomerCountry => CustomerCity.countryId == 0 ? new Country() : _countryData.GetCountryById(CustomerCity.countryId);
        public int CustomerId { get => _customerId; set => _customerId = value; }
    }
}
