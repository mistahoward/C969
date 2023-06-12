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
        public List<Customer> ConvertCustomerDataTableToList(DataTable dt)
        {
            List<Customer> customerList = new List<Customer>();
            foreach (DataRow row in dt.Rows)
            {
                var customerId = row.Field<int>("customerId");
                var customerName = row.Field<string>("customerName");
                var addressId = row.Field<int>("addressId");
                var active = Convert.ToBoolean(row["active"]);
                var createDate = row.Field<DateTime>("createDate");
                var createdBy = row.Field<string>("createdBy");
                var lastUpdate = row.Field<DateTime>("lastUpdate");
                var lastUpdateBy = row.Field<string>("lastUpdateBy");

                var workingCustomer = new Customer
                {
                    customerId = customerId,
                    customerName = customerName,
                    addressId = addressId,
                    active = active,
                    createDate = createDate,
                    createdBy = createdBy,
                    lastUpdate = lastUpdate,
                    lastUpdateBy = lastUpdateBy
                };
                customerList.Add(workingCustomer);
            }
            return customerList;
        }
        /// <summary>
        /// Gets a customer from the db by id
        /// </summary>
        /// <param name="id">ID of the customer - customerId in db</param>
        /// <returns>Customer if success, exception if fail</returns>
        /// <exception cref="DataNotFound"></exception>
        public Customer GetCustomerById(int id)
        {
            // Create an empty Customer and null result Customer
            Customer emptyCustomer = new Customer();
            Customer resultCustomer = null;
            // Call retrieve data, read the response and transpose it to the result customer
            DataTable dt = RetrieveData(emptyCustomer, "customerId", id);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                var customerId = row.Field<int>("customerId");
                var customerName = row.Field<string>("customerName");
                var addressId = row.Field<int>("addressId");
                var active = Convert.ToBoolean(row["active"]);
                var createDate = row.Field<DateTime>("createDate");
                var createdBy = row.Field<string>("createdBy");
                var lastUpdate = row.Field<DateTime>("lastUpdate");
                var lastUpdateBy = row.Field<string>("lastUpdateBy");

                resultCustomer = new Customer
                {
                    customerId = customerId,
                    customerName = customerName,
                    addressId = addressId,
                    active = active,
                    createDate = createDate,
                    createdBy = createdBy,
                    lastUpdate = lastUpdate,
                    lastUpdateBy = lastUpdateBy
                };
            }

            // If result customer exists, return it - otherwise, throw an exception
            if (resultCustomer == null)
            {
                throw new DataNotFound("No customer found with the provided ID.");
            }

            return resultCustomer;
        }
        /// <summary>
        /// Add Customer to db
        /// </summary>
        /// <param name="workingCustomer">Customer object to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="InvalidObject"></exception>
        public bool AddCustomer(Customer workingCustomer)
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
        /// Delete customer in db
        /// </summary>
        /// <param name="id">customerId to delete</param>
        /// <returns>Boolean of success</returns>
        public bool DeleteCustomerById(int id)
        {
            // Create appointment data object
            var appointmentAccess = new AppointmentData();

            bool deletionStatus = false;

            try
            {
                // Get customers appointments - need to do a cascade delete because of the foreign keys
                var customersAppointments = appointmentAccess.GetAppointmentsByCustomerId(id);

                // If the customer has associated appointments, delete all of the appointments
                if (customersAppointments.Count > 0)
                {
                    // Lambda used here for readibility and concicessness - also elimnates the need for a loop index
                    customersAppointments.ForEach((appointment) =>
                    {
                        appointmentAccess.DeleteAppointmentById(appointment.appointmentId);
                    });
                }

                // Delete the customer, assign the bool to deletionStatus for return
                deletionStatus = DeleteData<Customer>($"customerId = {id}");
            }
            catch
            {
                return deletionStatus;
            }

            return deletionStatus;
        }

        /// <summary>
        /// Get a list of customers
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetCustomers()
        {
            var customerDataTable = RetrieveData<Customer>();

            var customerList = ConvertCustomerDataTableToList(customerDataTable);

            return customerList;
        }
    }
}
