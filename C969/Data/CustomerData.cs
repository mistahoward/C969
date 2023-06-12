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
    public class CustomerData : Database
    {
        /// <summary>
        /// Gets a customer from the db by id
        /// </summary>
        /// <param name="id">ID of the customer</param>
        /// <returns>Customer if success, exception if fail</returns>
        /// <exception cref="DataNotFound"></exception>
        public Customer GetCustomerById(int id)
        {
            // Create an empty Customer
            Customer emptyCustomer = new Customer();
            // Call RetrieveSingleRow and transpose it to the result customer
            DataRow customerRow = RetrieveSingleRow(emptyCustomer, "customerId", id);
            Customer resultCustomer = DataTableConverter.ConvertDataRowToModel<Customer>(customerRow)
                ?? throw new DataNotFound("No customer found with the provided ID");
            return resultCustomer;
        }
        /// <summary>
        /// Gets a customer from the db by name
        /// </summary>
        /// <param name="customerName">Name of the customer</param>
        /// <returns>Customer if success, exception if fail</returns>
        /// <exception cref="DataNotFound"></exception>
        public Customer GetCustomerByName(string customerName)
        {
            Customer emptyCustomer = new Customer();
            DataRow customerRow = RetrieveSingleRow(emptyCustomer, "customerName", customerName);
            Customer resultCustomer = DataTableConverter.ConvertDataRowToModel<Customer>(customerRow)
                ?? throw new DataNotFound("No customer found with provided name");
            return resultCustomer;
        }
        /// <summary>
        /// Add Customer to db
        /// </summary>
        /// <param name="workingCustomer">Customer object to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="InvalidObject"></exception>
        public bool AddCustomer(Customer workingCustomer, Address workingAddress)
        {
            var validCustomer = ModelValidator.ValidateModel(workingCustomer);

            if (!validCustomer)
            {
                throw new InvalidObject("Customer isn't valid");
            }

            return AddData(workingCustomer);
        }
        /// <summary>
        /// Update customer in db
        /// </summary>
        /// <param name="workingCustomer">Customer object to update</param>
        /// <returns>Boolean of success</returns>
        public bool UpdateCustomer(Customer workingCustomer)
        {
            var validCustomer = ModelValidator.ValidateModel(workingCustomer);

            if (!validCustomer)
            {
                throw new InvalidObject("Customer isn't valid");
            }

            return UpdateData(workingCustomer, "customerId", workingCustomer.customerId);
        }
        /// <summary>
        /// Marks customer as inactive in db
        /// </summary>
        /// <param name="id">customerId to mark as inactive</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteCustomerById(int id)
        {
            try
            {
                Customer claimedCustomer = GetCustomerById(id);
                claimedCustomer.active = false;
                return UpdateData(claimedCustomer, "customerId", claimedCustomer.customerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong trying to delete the customer", ex);
            }
        }

        /// <summary>
        /// Get a list of customers
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetCustomers()
        {
            var customerDataTable = RetrieveData<Customer>();

            var customerList = DataTableConverter.ConvertDataTableToList<Customer>(customerDataTable);

            return customerList;
        }
    }
}
