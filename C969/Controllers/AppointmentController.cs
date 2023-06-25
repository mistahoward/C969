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
        private readonly List<Appointment> _weekAppointments;
        private readonly List<Appointment> _monthAppointments;

        public AppointmentController(DateTime requestedDate)
        {
            var appointmentData = new AppointmentData();
            var currentMonth = requestedDate.Month;
            var epochWeekNumber = EpochConverter.GetEpochWeekNumber(requestedDate);

            _weekAppointments = appointmentData.GetAppointmentsByWeek(epochWeekNumber);
            _monthAppointments = appointmentData.GetAppointmentsByMonth(currentMonth);
        }
        public List<Appointment> MonthAppointments { get { return _monthAppointments; } }
        public List<Appointment> WeekAppointments { get { return _weekAppointments; } }
    }
}
