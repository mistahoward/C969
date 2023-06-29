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

        ///<summary>
        ///Gets the list of appointments that occur in the provided month
        ///</summary>
        ///<param name="month">The month (1-12) for which to retrieve appointments</param>
        ///<returns>Returns a list of Appointment objects</returns>
        ///<exception cref="ArgumentOutOfRangeException">Thrown when month is less than 1 or greater than 12</exception>
        public List<Appointment> GetAppointmentsByMonth(int month)
        {
            // Check for a valid month
            if (month <= 0)
            {
                throw new ArgumentOutOfRangeException("Month cannot be less than or equal to 0");
            }
            if (month > 12)
            {
                throw new ArgumentOutOfRangeException("Month cannot be greater than 12");
            }

            // Initialize a new list of Appointment objects
            var appointmentList = new List<Appointment>();

            // Create a default Appointment object
            var emptyAppointment = new Appointment();

            // Retrieve appointments that start within the month
            DataTable appointmentStartDataTable = RetrieveData(emptyAppointment, "MONTH(start)", month);

            // Retrieve appointments that end within the month
            DataTable appointmentEndDataTable = RetrieveData(emptyAppointment, "MONTH(end)", month);

            // Merge both data tables
            appointmentStartDataTable.Merge(appointmentEndDataTable);

            // Convert merged data table to a list of appointments
            appointmentList = DataTableConverter.ConvertDataTableToList<Appointment>(appointmentStartDataTable);

            // Return the appointment list
            return appointmentList;
        }
        /// <summary>
        /// Gets the list of appointments that occur in the provided week number
        /// </summary>
        /// <param name="weekNumber">The week number for which to retrieve appointments</param>
        /// <returns>List of appointments if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when weekNumber is less than 1 or greater than 52</exception>
        public List<Appointment> GetAppointmentsByWeek(int weekNumber)
        {
            // Check for a valid week number
            if (weekNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("WeekNumber cannot be less than or equal to 0");
            }
            if (weekNumber > 52)
            {
                throw new ArgumentOutOfRangeException("WeekNumber cannot be greater than 52");
            }
            var startOfWeek = EpochConverter.GetStartOfWeek(weekNumber);
            var endOfWeek = startOfWeek.AddDays(7);

            // Initialize a new list of Appointment objects
            var appointmentList = new List<Appointment>();

            // Create a default Appointment object
            var emptyAppointment = new Appointment();

            // Retrieve appointments that start within the week, and consolidate the list
            var appointmentsByStartDate = DataTableConverter
                .ConvertDataTableToList<Appointment>(RetrieveData(emptyAppointment, "start", startOfWeek, endOfWeek))
                .GroupBy(appt => appt.appointmentId)
                .Select(group => group.First());

            // Retrieve appointments that end within the week
            var appointmentsByEndDate = DataTableConverter
                .ConvertDataTableToList<Appointment>(RetrieveData(emptyAppointment, "end", startOfWeek, endOfWeek))
                .GroupBy(appt => appt.appointmentId)
                .Select(group => group.First());

            // Add the consolidated list to the appointmentList object to prevent duplicates
            var consolidatedAppointments = appointmentsByStartDate
                .GroupBy(appt => appt.appointmentId)
                .Select(group => group.First());
            appointmentList.AddRange(consolidatedAppointments);

            // Return the consolidated list
            return appointmentList;
        }
    }
}
