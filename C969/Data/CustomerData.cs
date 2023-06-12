﻿using C969.Exceptions;
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
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            Customer emptyCustomer = new Customer();
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
            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException($"'{nameof(customerName)}' cannot be null or whitespace", nameof(customerName));
            }

            Customer emptyCustomer = new Customer();
            DataRow customerRow = RetrieveSingleRow(emptyCustomer, "customerName", customerName);
            Customer resultCustomer = DataTableConverter.ConvertDataRowToModel<Customer>(customerRow)
                ?? throw new DataNotFound("No customer found with provided name");
            return resultCustomer;
        }
        /// <summary>
        /// Checks if a customer exists in the database based on name and id
        /// </summary>
        /// <param name="workingCustomer">The customer instance to check</param>
        /// <returns>true if the customer exists, false otherwise</returns>
        public bool DoesCustomerExist(Customer workingCustomer)
        {
            try
            {
                Customer customerByNameSearch = GetCustomerByName(workingCustomer.customerName);
                Customer customerByIdSearch = GetCustomerById(workingCustomer.customerId);
                return true;
            }
            catch (DataNotFound)
            {
                return false;
            }
        }
        /// <summary>
        /// Add Customer to db
        /// </summary>
        /// <param name="workingCustomer">Customer object to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="InvalidObject"></exception>
        public bool AddCustomer(Customer workingCustomer, Address workingAddress)
        {
            var existingCustomer = DoesCustomerExist(workingCustomer);

            if (existingCustomer)
            {
                throw new DuplicateData("Customer already exists!");
            }

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
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
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
        /// Get a list of active customers
        /// </summary>
        /// <returns>List of active customers</returns>
        public List<Customer> GetCustomers()
        {
            var customerDataTable = RetrieveData<Customer>();

            var customerList = DataTableConverter.ConvertDataTableToList<Customer>(customerDataTable);

            // lambda for readability and concisesness
            var activeCustomers = customerList.Where(c => c.active).ToList();

            return activeCustomers;
        }
    }
}
