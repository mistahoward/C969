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
        /// <exception cref="DataNotFound"></exception>
        public Address GetAddressById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            Address emptyAddress = new Address();
            DataRow addressRow = RetrieveSingleRow(emptyAddress, "addressId", id);
            Address resultAddress = DataTableConverter.ConvertDataRowToModel<Address>(addressRow)
                ?? throw new DataNotFound("No address found with the provided ID");
            return resultAddress;
        }
        /// <summary>
        /// Gets an address from the db by name
        /// </summary>
        /// <param name="addressName">Name of the Address (Address.address)</param>
        /// <returns>Address if success, exception if fail</returns>
        /// <exception cref="DataNotFound"></exception>
        public Address GetAddressByName(string addressName)
        {
            if (string.IsNullOrWhiteSpace(addressName))
            {
                throw new ArgumentException($"'{nameof(addressName)}' cannot be null or whitespace", nameof(addressName));
            }
            Address emptyAddress = new Address();
            DataRow addressRow = RetrieveSingleRow(emptyAddress, "address", addressName);
            Address resultAddress = DataTableConverter.ConvertDataRowToModel<Address>(addressRow)
                ?? throw new DataNotFound("No address found with provided Name");
            return resultAddress;
        }
        /// <summary>
        /// Checks if an address exists in the database based on name and id
        /// </summary>
        /// <param name="workingAddress">The address instance to check</param>
        /// <returns>true if the address exists, false otherwise</returns>
        public bool DoesAddressExist(Address workingAddress)
        {
            try
            {
                Address addressByNameSearch = GetAddressByName(workingAddress.address);
                Address addressByIdSearch = GetAddressById(workingAddress.addressId);
                return true;
            }
            catch (DataNotFound)
            {
                return false;
            }
        }
        public bool AddAddress(Address workingAddress)
        {
            var validAddress = ModelValidator.ValidateModel(workingAddress);

            if (!validAddress)
            {
                throw new InvalidObject("Address isn't valid");
            }

            return AddData(workingAddress);
        }
        public bool UpdateAddress(Address workingAddress)
        {
            var validAddress = ModelValidator.ValidateModel(workingAddress);

            if (!validAddress)
            {
                throw new InvalidObject("Address isn't valid");
            }

            return UpdateData(workingAddress, "addressId", workingAddress.addressId);
        }
        public bool DeleteAddressById(int id)
        {
            return DeleteData<Address>($"addressId = {id}");
        }
    }
}
