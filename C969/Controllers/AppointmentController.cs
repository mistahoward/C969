using C969.Data;
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
        public List<Appointment> MonthAppointments => _monthAppointments;
        public List<Appointment> WeekAppointments => _weekAppointments;
        public Appointment Appointment => AppointmentId == 0 ? new Appointment() : _appointmentData.GetAppointmentById(AppointmentId);
        public int AppointmentId { get => _appointmentId; set => _appointmentId = value; }
    }
}
