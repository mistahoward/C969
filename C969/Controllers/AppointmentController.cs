using C969.Data;
using C969.Exceptions;
using C969.Models;
using C969.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Controllers
{
    public class AppointmentController
    {
        private readonly AppointmentData _appointmentData;
        private List<Appointment> _weekAppointments;
        private List<Appointment> _monthAppointments;
        private int _appointmentId;

        public AppointmentController()
        {
            _appointmentData = new AppointmentData();
        }

        /// <summary>
        /// Set week appointments for the given epoch week number
        /// </summary>
        /// <param name="weekNum">The epoch week number</param>
        public void SetWeekAppointments(int weekNum = -1)
        {
            _weekAppointments = _appointmentData.GetAppointmentsByWeek(weekNum);
        }

        /// <summary>
        /// Set month appointments for the given month of the year
        /// </summary>
        /// <param name="monthNum">The month of the year</param>
        public void SetMonthAppointments(int monthNum = -1)
        {
            _monthAppointments = _appointmentData.GetAppointmentsByMonth(monthNum);
        }
        public List<AppointmentMeta> ConvertAppointmentsToAppointmentMeta(List<Appointment> workingAppointments)
        {
            var appointmentMetas = new List<AppointmentMeta>();
            foreach (var appointment in workingAppointments)
            {
                var appointmentMeta = new AppointmentMeta
                {
                    appointmentId = appointment.appointmentId,
                    title = appointment.title,
                    description = appointment.description,
                    location = appointment.location,
                    type = appointment.type,
                    start = EpochConverter.ConvertUtcToUserTimeWithTimeZone(appointment.start),
                    end = EpochConverter.ConvertUtcToUserTimeWithTimeZone(appointment.end),
                };
                appointmentMetas.Add(appointmentMeta);
            }
            return appointmentMetas;
        }
        private bool UpdateAppointmentInWeek(Appointment workingAppointment)
        {
            if (WeekAppointments == null) return false;

            var currentIndex = WeekAppointments.FindIndex(x => x.appointmentId == workingAppointment.appointmentId);

            if (currentIndex >= 0)
            {
                WeekAppointments[currentIndex] = workingAppointment;
                return true;
            }

            return false;
        }

        private bool UpdateAppointmentInMonth(Appointment workingAppointment)
        {
            if (MonthAppointments == null) return false;

            var currentIndex = MonthAppointments.FindIndex(x => x.appointmentId == workingAppointment.appointmentId);

            if (currentIndex >= 0)
            {
                MonthAppointments[currentIndex] = workingAppointment;
                return true;
            }

            return false;
        }
        private bool AddAppointmentToWeek(Appointment workingAppointment)
        {
            if (WeekAppointments == null) return false;

            if (EpochConverter.IsInCurrentWeek(workingAppointment.start) || EpochConverter.IsInCurrentWeek(workingAppointment.end))
            {
                WeekAppointments.Add(workingAppointment);
                return true;
            }

            return false;
        }
        private bool AddAppointmentToMonth(Appointment workingAppointment)
        {
            if (MonthAppointments == null) return false;

            if (EpochConverter.IsInCurrentMonth(workingAppointment.start) || EpochConverter.IsInCurrentMonth(workingAppointment.end))
            {
                MonthAppointments.Add(workingAppointment);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Updates an appointment
        /// </summary>
        /// <param name="workingAppointment">The appointment to update</param>
        /// <returns>True if the update is successful; false otherwise</returns>
        public bool HandleUpdateAppointment(Appointment workingAppointment)
        {
            if (workingAppointment.Equals(Appointment))
            {
                return false;
            }
            var validAppointment = ModelValidator.ValidateModel(workingAppointment);
            if (!validAppointment)
            {
                throw new InvalidObject("Appointment is missing required data");
            }
            try
            {
                workingAppointment.UpdateAppointment();
                var result = _appointmentData.UpdateAppointment(workingAppointment);
                if (result)
                {
                    bool updatedInWeek = UpdateAppointmentInWeek(workingAppointment);
                    bool updatedInMonth = UpdateAppointmentInMonth(workingAppointment);

                    if (updatedInWeek || updatedInMonth)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool HandleAddAppointment(Appointment workingAppointment)
        {
            var validAppointment = ModelValidator.ValidateModel(workingAppointment);
            if (!validAppointment)
            {
                throw new InvalidObject("Appointment is missing required data");
            }
            try
            {
                workingAppointment.UpdateAppointment();
                var result = _appointmentData.AddAppointment(workingAppointment);
                if (result)
                {
                    bool addedToWeek = AddAppointmentToWeek(workingAppointment);
                    bool addedToMonth = AddAppointmentToMonth(workingAppointment);
                    if (addedToWeek || addedToMonth)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during the update
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }

            return false;
        }

        public bool HandleDeleteAppointment(int appointmentId)
        {
            try
            {
                var claimedAppointment = _appointmentData.GetAppointmentById(appointmentId);
                var result = _appointmentData.DeleteAppointmentById(claimedAppointment.appointmentId);
                if (result)
                {
                    return true;
                }
                return false;
            } catch (DataNotFound ex)
            {
                throw ex;
            }
        }
        public List<Appointment> MonthAppointments => _monthAppointments;
        public List<Appointment> WeekAppointments => _weekAppointments;
        public Appointment Appointment => AppointmentId == 0 ? new Appointment() : _appointmentData.GetAppointmentById(AppointmentId);
        public int AppointmentId { get => _appointmentId; set => _appointmentId = value; }
    }
}
