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
    public class AppointmentData : Database
    {
        /// <summary>
        /// Converts a DataTable to a List of Appointments
        /// </summary>
        /// <param name="dt">The DataTable to convert</param>
        /// <returns>List of Appointment(s)</returns>
        public List<Appointment> ConvertAppointmentDataTableToList(DataTable dt)
        {
            List<Appointment> appointmentList = new List<Appointment>();

            foreach (DataRow row in dt.Rows)
            {
                var appointmentId = row.Field<int>("appointmentId");
                var customerId = row.Field<int>("customerId");
                var title = row.Field<string>("title");
                var description = row.Field<string>("description");
                var location = row.Field<string>("location");
                var contact = row.Field<string>("contact");
                var type = row.Field<string>("type");
                var url = row.Field<string>("url");
                var start = row.Field<DateTime>("start");
                var end = row.Field<DateTime>("end");
                var createDate = row.Field<DateTime>("createDate");
                var createdBy = row.Field<string>("createdBy");
                var lastUpdate = row.Field<DateTime>("lastUpdate");
                var lastUpdateBy = row.Field<string>("lastUpdateBy");

                var workingAppointment = new Appointment
                {
                    appointmentId = appointmentId,
                    customerId = customerId,
                    title = title,
                    description = description,
                    location = location,
                    contact = contact,
                    type = type,
                    url = url,
                    start = start,
                    end = end,
                    createDate = createDate,
                    createdBy = createdBy,
                    lastUpdate = lastUpdate,
                    lastUpdateBy = lastUpdateBy,
                };

                appointmentList.Add(workingAppointment);
            }

            return appointmentList;
        }
        /// <summary>
        /// Gets an appointment from the db by id
        /// </summary>
        /// <param name="id">ID of the appointment - appointmentId in db</param>
        /// <returns>List of papointments if success, exception if fail</returns>
        /// <exception cref="DataNotFound"></exception>
        public List<Appointment> GetAppointmentsByCustomerId(int id)
        {
            var emptyAppointment = new Appointment();

            var customerAccess = new CustomerData();

            try
            {
                var claimedCustomer = customerAccess.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                throw new DataNotFound("Customer not found", ex);
            }

            var customerAppointmentsDataTable = RetrieveData(emptyAppointment, "customerId", id);

            var appointmentList = ConvertAppointmentDataTableToList(customerAppointmentsDataTable);

            return appointmentList;
        }
        /// <summary>
        /// Add Appointment to db
        /// </summary>
        /// <param name="workingAppointment">Appointment to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="InvalidObject"></exception>
        public bool AddAppointment(Appointment workingAppointment)
        {
            var validAppointment = ModelValidator.ValidateModel(workingAppointment);

            if (!validAppointment)
            {
                throw new InvalidObject("Appointment isn't valid");
            }

            return AddData(workingAppointment);
        }
        /// <summary>
        /// Delete appointment in db
        /// </summary>
        /// <param name="id">appointmentId to delete</param>
        /// <returns>Boolean of success</returns>
        public bool DeleteAppointmentById(int id)
        {
            return DeleteData<Appointment>($"appointmentId = {id}");
        }
        /// <summary>
        /// Update appointment in db
        /// </summary>
        /// <param name="workingAppointment">Appointment object to update</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="InvalidObject"></exception>
        public bool UpdateAppointment(Appointment workingAppointment)
        {
            var validAppointment = ModelValidator.ValidateModel(workingAppointment);

            if (!validAppointment)
            {
                throw new InvalidObject("Appointment isn't valid");
            }

            return UpdateData(workingAppointment, "appointmentId", workingAppointment.appointmentId);
        }
    }
}
