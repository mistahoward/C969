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
        public Customer GetCustomerById(int id)
        {
            Customer emptyCustomer = new Customer();
            Customer resultCustomer = null;

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

            // If result user exists, return it - otherwise, throw an exception
            if (resultCustomer == null)
            {
                throw new Exception("No user found with the provided ID.");
            }

            return resultCustomer;
        }
    }
}
