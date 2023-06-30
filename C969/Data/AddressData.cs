using C969.Exceptions;
using C969.Models;
using C969.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Data
{
    public class AddressData : Database
    {
        /// <summary>
        /// Gets an address from the db by id
        /// </summary>
        /// <param name="id">ID of the address</param>
        /// <returns>Address if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when address object is not found</exception>
        public Address GetAddressById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            Address emptyAddress = new Address();
            DataRow addressRow = RetrieveSingleRow(emptyAddress, "addressId", id) ?? throw new DataNotFound("No address found with the provided ID");
            Address resultAddress = DataTableConverter.ConvertDataRowToModel<Address>(addressRow);
            return resultAddress;
        }
        /// <summary>
        /// Gets an address from the db by name
        /// </summary>
        /// <param name="addressName">Name of the Address (Address.address)</param>
        /// <returns>Address if success, exception if fail</returns>
        /// <exception cref="ArgumentException">Thrown when address name is null or whitespace</exception>
        /// <exception cref="DataNotFound">Thrown when address object is not found</exception>
        public Address GetAddressByName(string addressName)
        {
            if (string.IsNullOrWhiteSpace(addressName))
            {
                throw new ArgumentException($"'{nameof(addressName)}' cannot be null or whitespace", nameof(addressName));
            }
            Address emptyAddress = new Address();
            DataRow addressRow = RetrieveSingleRow(emptyAddress, "address", addressName) ?? throw new DataNotFound("No address found with provided name");
            Address resultAddress = DataTableConverter.ConvertDataRowToModel<Address>(addressRow);
            return resultAddress;
        }
        /// <summary>
        /// Determines whether an address is attached to any customers
        /// </summary>
        /// <param name="id">ID of the address</param>
        /// <returns>True if the address is attached to at least one customer, otherwise false</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when address object is not found with provided id</exception>
        public bool AddressAttachedToCustomers(int id)
        {
            bool addressBeingUsed = false;
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            try
            {
                var claimedAddress = GetAddressById(id);
                var customerDataAccess = new CustomerData();
                var customerAddressLookUp = customerDataAccess.GetCustomersByAddressId(id);
                if (customerAddressLookUp.Count < 0)
                {
                    addressBeingUsed = true;
                }
            }
            catch (Exception ex)
            {
                if (ex is DataNotFound)
                {
                    throw ex;
                }
                else
                {
                    throw new Exception("Something went wrong checking if the address is attached to any customers");
                }
            }
            return addressBeingUsed;
        }
        /// <summary>
        /// Retrieves a list of addresses associated with a city id
        /// </summary>
        /// <param name="id">ID of the city</param>
        /// <returns>A list of addresses associated with the city ID</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        public List<Address> GetAddressByCityId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            var emptyAddress = new Address();
            var addressDataTable = RetrieveData(emptyAddress, "cityId", id);
            return DataTableConverter.ConvertDataTableToList<Address>(addressDataTable);
        }
        /// <summary>
        /// Checks if an address exists in the database based on id
        /// </summary>
        /// <param name="workingAddress">The address instance to check</param>
        /// <returns>true if the address exists, false otherwise</returns>
        public bool DoesAddressExist(Address workingAddress)
        {
            try
            {
                Address addressByIdSearch = GetAddressById(workingAddress.addressId);
                return true;
            }
            catch (DataNotFound)
            {
                return false;
            }
        }
        /// <summary>
        /// Method to check if a duplicate address exists in the database
        /// </summary>
        /// <param name="workingAddress">The address to check for duplicates</param>
        /// <returns>AddressId if duplicate exists, 0 otherwise.</returns>
        public int DoesDuplicateExist(Address workingAddress)
        {
            try
            {
                Address addressByNameSearch = GetAddressByName(workingAddress.address);
                return addressByNameSearch.addressId;
            }
            catch (DataNotFound)
            {
                return 0;
            }
        }
        /// <summary>
        /// Add address to db
        /// </summary>
        /// <param name="workingAddress">Address object to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DuplicateData">Thrown when the address already exists</exception>
        /// <exception cref="InvalidObject">Thrown when the address object is not valid</exception>
        public bool AddAddress(Address workingAddress)
        {
            bool validAddress = ModelValidator.ValidateModel(workingAddress);
            if (!validAddress)
            {
                throw new InvalidObject("Address isn't valid");
            }

            bool existingAddress = DoesAddressExist(workingAddress);

            if (existingAddress)
            {
                throw new DuplicateData("Address already exists");
            }

            return AddData(workingAddress);
        }
        /// <summary>
        /// Update address in db
        /// </summary>
        /// <param name="workingAddress">Address object to update</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DataNotFound">Thrown when the address object cannot be found by ID</exception>
        /// <exception cref="DuplicateData">Thrown when an address object is found with the same "name" (address.address)</exception>
        /// <exception cref="InvalidObject">Thrown when the address object is not valid</exception>
        public bool UpdateAddress(Address workingAddress)
        {
            bool validAddress = ModelValidator.ValidateModel(workingAddress);
            if (!validAddress)
            {
                throw new InvalidObject("Address isn't valid");
            }
            bool existingAddress = DoesAddressExist(workingAddress);
            if (!existingAddress)
            {
                throw new DataNotFound("Address does not exist");
            }
            int duplicateAddress = DoesDuplicateExist(workingAddress);
            if (duplicateAddress != 0)
            {
                throw new DuplicateData($"Address already exists by name - please use {duplicateAddress}.", duplicateAddress);
            }

            return UpdateData(workingAddress, "addressId", workingAddress.addressId);
        }
        /// <summary>
        /// Deletes an address from the database by id
        /// </summary>
        /// <param name="id">id of the address to delete</param>
        /// <returns>True if the address was successfully deleted, False otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="ChangeNotPermitted">Thrown when address is still attached to customers (foreign key constraint)</exception>
        /// <exception cref="DataNotFound">Thrown when address object cannot be found by provided id</exception>
        public bool DeleteAddressById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }

            bool addressBeingUsed = AddressAttachedToCustomers(id);
            if (addressBeingUsed)
            {
                throw new ChangeNotPermitted("Address attached to customers");
            }

            Address claimedAddress = GetAddressById(id);
            bool existingAddress = DoesAddressExist(claimedAddress);

            if (!existingAddress)
            {
                throw new DataNotFound("Address does not exist");
            }

            return DeleteData<Address>($"addressId = {id}");
        }
    }
}
