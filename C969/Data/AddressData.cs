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
        public Address GetAddressByAddressString(string address)
        {
            var emptyAddress = new Address();
            try
            {
                var attempedAddress = RetrieveData(emptyAddress, "address", address);

            } catch (Exception ex)
            {
                throw new Exception("No data found", ex);
            }
        }
    }
}
