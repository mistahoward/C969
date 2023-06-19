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

            var appointmentList = DataTableConverter.ConvertDataTableToList<Appointment>(customerAppointmentsDataTable);

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
