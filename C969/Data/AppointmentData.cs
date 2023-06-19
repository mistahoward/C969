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
        /// Gets a list of appointments from the db by customer id
        /// </summary>
        /// <param name="id">Customer id to look up</param>
        /// <returns>List of appointments if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// 
        public List<Appointment> GetAppointmentsByCustomerId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            var emptyAppointment = new Appointment();

            var customerAppointmentsDataTable = RetrieveData(emptyAppointment, "customerId", id);

            var appointmentList = DataTableConverter.ConvertDataTableToList<Appointment>(customerAppointmentsDataTable);

            return appointmentList;
        }
        /// <summary>
        /// Gets a list of appointments from teh db by user id 
        /// </summary>
        /// <param name="id">User id to look up</param>
        /// <returns>List of appointments if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        public List<Appointment> GetAppointmentsByUserId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            var emptyAppointment = new Appointment();

            var userAppointmentsDataTable = RetrieveData(emptyAppointment, "userId", "id");

            var appointmentList = DataTableConverter.ConvertDataTableToList<Appointment>(userAppointmentsDataTable);

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
