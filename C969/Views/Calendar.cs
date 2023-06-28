using C969.Controllers;
using C969.Models;
using C969.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969
{
    public partial class Calendar : Form
    {
        private readonly AppointmentController _appointmentController;
        public List<Appointment> Appointments { get; set; }
        public BindingList<string> Weeks { get; set; }
        public BindingList<string> Months { get; set;  }
        public Calendar(DateTime requestedDate, ViewType requestedView)
        {
            InitializeComponent();

            _appointmentController = new AppointmentController(requestedDate);

            Weeks = new BindingList<string>();
            Months = new BindingList<string>();

            PopulateWeeks(requestedDate);
            PopulateMonths();

            switch (requestedView)
            {
                case ViewType.Week:
                    Appointments = _appointmentController.WeekAppointments;
                    weekMonthComboBox.DataSource = Weeks;
                    break;
                case ViewType.Month:
                    Appointments = _appointmentController.MonthAppointments;
                    weekMonthComboBox.DataSource = Months;
                    break;
                default:
                    throw new ArgumentException("Invalid view type specified");
            }
            AppointmentDataGridView.DataSource = Appointments;
        }
        /// <summary>
        /// Populates the Weeks BindingList with 52 week entries and sets the weekMonthComboBox.SelectedItem as the requestedDate 
        /// if it falls within the weeks list.
        /// </summary>
        /// <param name="requestedDate">A DateTime representing the requested date for the calendar</param>
        private async void PopulateWeeks(DateTime requestedDate)
        {
            for (int i = 1; i <= 52; i++)
            {
                var startOfWeek = EpochConverter.GetStartOfWeek(i);
                var endOfWeek = startOfWeek.AddDays(6);
                string weekEntry = $"Week #{i} ({startOfWeek:MM/dd/yyyy} - {endOfWeek:MM/dd/yyyy})";
                Weeks.Add(weekEntry);
            }

            int requestedWeekNumber = EpochConverter.GetEpochWeekNumber(requestedDate);
            string requestedWeekEntry = Weeks.FirstOrDefault(w => w.StartsWith($"Week #{requestedWeekNumber}"));

            if (requestedWeekEntry != null)
            {
                // This delay is necessary to make sure the selection has time to catch up with the new list.
                await Task.Delay(100);
                weekMonthComboBox.SelectedItem = requestedWeekEntry;
                weekMonthComboBox.Update();
            }
        }
        private void PopulateMonths()
        {
            for (int i = 1; i <= 12; i++)
            {
                string monthEntry = $"Month #{i}";
                Months.Add(monthEntry);
            }
        }
    }
}
