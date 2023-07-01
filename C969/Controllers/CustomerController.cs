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
        /// Adds a customer to the database.
        /// </summary>
        /// <param name="workingCustomer">The customer to be added.</param>
        /// <returns>ID of the new customer - 0 if fail</returns>
        public int HandleAddCustomer(Customer workingCustomer)
        {
            var validCustomer = ModelValidator.ValidateModel(workingCustomer);
            if (!validCustomer)
            {
                throw new InvalidObject("Customer is missing required data");
            }
            try
            {
                workingCustomer.UpdateCustomer();
                var result = _customerData.AddCustomer(workingCustomer);
                if (result != 0)
                {
                    workingCustomer.customerId = result;
                    WriteActivityLog.Write($"{workingCustomer.customerId} has been created");
                    _customers.Add(workingCustomer);
                    return result;
                }
                return 0;
            }
            catch (DuplicateData ex)
            {
                return ex.DuplicateId;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Handles adding an address to the database
        /// </summary>
        /// <param name="workingAddress">The address to be added</param>
        /// <returns>True if the address is added successfully, false otherwise</returns>
        public int HandleAddAddress(Address workingAddress)
        {
            var validAddress = ModelValidator.ValidateModel(workingAddress);
            if (!validAddress)
            {
                throw new InvalidObject("Address is missing required data");
            }
            try
            {
                workingAddress.UpdateAddress();
                int duplicateAddress = _addressData.DoesDuplicateExist(workingAddress);
                if (duplicateAddress != 0)
                {
                    throw new DuplicateData($"Address already exists - use ${duplicateAddress}", duplicateAddress);
                }
                var result = _addressData.AddAddress(workingAddress);
                WriteActivityLog.Write($"{workingAddress.addressId} has been created");
                return result;
            }
            catch (DuplicateData ex)
            {
                return ex.DuplicateId;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Handles the adding of a city to the database
        /// </summary>
        /// <param name="workingCity">The city to add to the database</param>
        /// <returns>True if the city is added, false otherwise</returns>
        public int HandleAddCity(City workingCity)
        {
            var validCity = ModelValidator.ValidateModel(workingCity);
            if (!validCity)
            {
                throw new InvalidObject("City is missing required data");
            }
            try
            {
                workingCity.UpdateCity();
                int duplicateCity = _cityData.DoesDuplicateExist(workingCity);
                if (duplicateCity != 0)
                {
                    throw new DuplicateData($"City already exists - use ${duplicateCity}", duplicateCity);
                }
                var result = _cityData.AddCity(workingCity);
                WriteActivityLog.Write($"{workingCity.cityId} has been created");
                return result;
            }
            catch (DuplicateData ex)
            {
                return ex.DuplicateId;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Handles adding a new country to the system
        /// </summary>
        /// <param name="workingCountry">The new country to add</param>
        /// <returns>True if the addition was successful, false if it failed</returns>
        public int HandleAddCountry(Country workingCountry)
        {
            var validCountry = ModelValidator.ValidateModel(workingCountry);
            if (!validCountry)
            {
                throw new InvalidObject("Country is missing required data");
            }
            try
            {
                workingCountry.UpdateCountry();
                int duplicateCountry = _countryData.DoesDuplicateExist(workingCountry);
                if (duplicateCountry != 0)
                {
                    throw new DuplicateData($"Country already exists - use ${duplicateCountry}", duplicateCountry);
                }
                var result = _countryData.AddCountry(workingCountry);
                WriteActivityLog.Write($"{workingCountry.countryId} has been created");
                return result;
            }
            catch (DuplicateData ex)
            {
                return ex.DuplicateId;
            }
            catch
            {
                return 0;
            }
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
                    WriteActivityLog.Write($"{workingCustomer.customerId} has been updated");
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
                WriteActivityLog.Write($"{workingCustomerAddress.addressId} has been updated");
                return result;
            }
            catch (DuplicateData ex)
            {
                Address duplicateAddress = _addressData.GetAddressById(ex.DuplicateId);
                Customer updatedCustomer = Customer;
                updatedCustomer.addressId = duplicateAddress.addressId;
                var result = _customerData.UpdateCustomer(updatedCustomer);
                WriteActivityLog.Write($"{updatedCustomer.customerId} has been updated");

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
                WriteActivityLog.Write($"{workingCustomerCity.cityId} has been updated");
                return result;
            }
            catch (DuplicateData ex)
            {
                City duplicateCity = _cityData.GetCityById(ex.DuplicateId);
                Address updatedAddress = CustomerAddress;
                updatedAddress.cityId = duplicateCity.cityId;
                var result = _addressData.UpdateAddress(updatedAddress);
                WriteActivityLog.Write($"{updatedAddress.addressId} has been updated");
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
                WriteActivityLog.Write($"{workingCustomerCountry.countryId} has been updated");
                return result;
            }
            catch (DuplicateData ex)
            {
                Country duplicateCountry = _countryData.GetCountryById(ex.DuplicateId);
                City updatedCity = CustomerCity;
                updatedCity.countryId = duplicateCountry.countryId;
                var result = _cityData.UpdateCity(updatedCity);
                WriteActivityLog.Write($"{updatedCity.cityId} has been updated");
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
