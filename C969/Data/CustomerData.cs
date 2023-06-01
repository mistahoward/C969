using C969.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Data
{
    public class CustomerData : Database
    {
        /// <summary>
        /// Gets a user from the DB by id
        /// </summary>
        /// <param name="id">ID of the customer - customerId in db</param>
        /// <returns>Customer if success, exception if fail</returns>
        /// <exception cref="Exception"></exception>
        public Customer GetCustomerById(int id)
        {
            // Create empty customer and null result customer
            Customer emptyCustomer = new Customer();
            Customer resultCustomer = null;

            // Retrieve Data using parameter, assign to result customer
            using (var sqlResponse = RetrieveData(emptyCustomer, "customerId", id))
            {
                if (sqlResponse.Read())
                {
                    resultCustomer = new Customer
                    {
                        customerId = sqlResponse.GetInt32(sqlResponse.GetOrdinal("customerId")),
                        customerName = sqlResponse.GetString(sqlResponse.GetOrdinal("customerName")),
                        addressId = sqlResponse.GetInt32(sqlResponse.GetOrdinal("addressId")),
                        active = sqlResponse.GetBoolean(sqlResponse.GetOrdinal("active")),
                        createDate = sqlResponse.GetDateTime(sqlResponse.GetOrdinal("createDate")),
                        createdBy = sqlResponse.GetString(sqlResponse.GetOrdinal("createdBy")),
                        lastUpdate = sqlResponse.GetDateTime(sqlResponse.GetOrdinal("lastUpdate")),
                        lastUpdateBy = sqlResponse.GetString(sqlResponse.GetOrdinal("lastUpdateBy"))
                    };
                }
            }

            // If result customer exists, return it - otherwise, throw an exception
            if (resultCustomer == null)
            {
                throw new Exception("No user found with the provided ID.");
            }

            return resultCustomer;
        }
        /// <summary>
        /// Add Customer to db
        /// </summary>
        /// <param name="workingCustomer">Customer object to add</param>
        /// <returns>Boolean of success</returns>
        public bool AddCustomer(Customer workingCustomer)
        {
            return AddData(workingCustomer);
        }
    }
}
