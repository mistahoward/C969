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
        public int RequestedWeekNumber { get; set; }
        public int RequestedMonthNumber { get; set; }
        public Calendar(DateTime requestedDate, ViewType requestedView)
        {
            InitializeComponent();

            _appointmentController = new AppointmentController();

            Weeks = new BindingList<string>();
            Months = new BindingList<string>();

            var currentWeekNumber = EpochConverter.GetEpochWeekNumber(requestedDate);
            RequestedWeekNumber = currentWeekNumber;
            RequestedMonthNumber = requestedDate.Month;

            switch (requestedView)
            {
                case ViewType.Week:
                    ChangeToWeekView(currentWeekNumber);
                    PopulateWeeks();
                    break;
                case ViewType.Month:
                    ChangeToMonthView(requestedDate.Month);
                    PopulateMonths();
                    break;
                default:
                    throw new ArgumentException("Invalid view type specified");
            }
            AppointmentDataGridView.DataSource = Appointments;
        }
        /// <summary>
        /// Updates the list of appointments displayed in the calendar by filtering for appointments in the specified week number.
        /// </summary>
        /// <param name="workingWeekNumber">The number of the week to display appointments for.</param>
        private void UpdateAppointmentsByWeek(int workingWeekNumber)
        {
            RequestedWeekNumber = workingWeekNumber;
            _appointmentController.SetWeekAppointments(workingWeekNumber);
            Appointments = _appointmentController.WeekAppointments;
            AppointmentDataGridView.DataSource = Appointments;
            weekMonthComboBox.DataSource = Weeks;
        }
        /// <summary>
        /// Changes the view to a weekly view in the calendar.
        /// </summary>
        private void ChangeToWeekView(int requestedWeek)
        {
            UpdateAppointmentsByWeek(requestedWeek);
            weekMonthLabel.Text = "Week Range";
            weekMonthToggle.Text = "Weeks";
            weekMonthToggle.Checked = true;
        }
        /// <summary>
        /// Updates the list of appointments displayed in the calendar by filtering for appointments in the specified month number.
        /// </summary>
        /// <param name="workingMonthNumber">The number of the month to display appointments for.</param>
        private void UpdateAppointmentsByMonth(int workingMonthNumber)
        {
            RequestedMonthNumber = workingMonthNumber;
            _appointmentController.SetMonthAppointments(workingMonthNumber);
            Appointments = _appointmentController.MonthAppointments;
            AppointmentDataGridView.DataSource = Appointments;
            weekMonthComboBox.DataSource = Months;
        }
        /// <summary>
        /// Changes the view to a monthly view in the calendar.
        /// </summary>
        private void ChangeToMonthView(int requestedMonth)
        {
            UpdateAppointmentsByMonth(requestedMonth);
            weekMonthLabel.Text = "Month Range";
            weekMonthToggle.Text = "Months";
            weekMonthToggle.Checked = false;
        }
        /// <summary>
        /// Populates the Weeks BindingList with 52 week entries and sets the weekMonthComboBox.SelectedItem as the requestedDate 
        /// if it falls within the weeks list.
        /// </summary>
        private async void PopulateWeeks()
        {
            for (int i = 1; i <= 52; i++)
            {
                var startOfWeek = EpochConverter.GetStartOfWeek(i);
                var endOfWeek = startOfWeek.AddDays(6);
                string weekEntry = $"Week #{i} ({startOfWeek:MM/dd/yyyy} - {endOfWeek:MM/dd/yyyy})";
                Weeks.Add(weekEntry);
            }
            string requestedWeekEntry = Weeks.FirstOrDefault(w => w.StartsWith($"Week #{RequestedWeekNumber}"));

            if (requestedWeekEntry != null)
            {
                // This delay is necessary to make sure the selection has time to catch up with the new list.
                await Task.Delay(100);
                weekMonthComboBox.SelectedItem = requestedWeekEntry;
                weekMonthComboBox.Update();
            }
        }
        /// <summary>
        /// Populates the Months BindingList with 12 month entries.
        /// </summary>
        private void PopulateMonths()
        {
            string january = "January";
            string february = "February";
            string march = "March";
            string april = "April";
            string may = "May";
            string june = "June";
            string july = "July";
            string august = "August";
            string september = "September";
            string october = "October";
            string november = "November";
            string december = "December";

            var monthsList = new List<string>
            {
                $"{january}",
                $"{february}",
                $"{march}",
                $"{april}",
                $"{may}",
                $"{june}",
                $"{july}",
                $"{august}",
                $"{september}",
                $"{october}",
                $"{november}",
                $"{december}"
            };

            foreach (string month in monthsList)
            {
                Months.Add(month);
            }
        }
        /// <summary>
        /// Event handler for the check changed event of the weekMonthToggle control. Updates the calendar view to display either a monthly or weekly view depending on toggle state.
        /// </summary>
        /// <param name="sender">The control that triggered this event.</param>
        /// <param name="e">The event arguments.</param>
        private void weekMonthToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (weekMonthToggle.Checked)
            {
                ChangeToWeekView(RequestedWeekNumber);
            }
            else
            {
                ChangeToMonthView(RequestedMonthNumber);
            }
        }

        /// <summary>
        /// Handles the event when the user changes the selected index of the week/month combo box.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void weekMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (weekMonthToggle.Checked)
            {
                UpdateAppointmentsByWeek(weekMonthComboBox.SelectedIndex + 1);
            }
            else
            {
                UpdateAppointmentsByMonth(weekMonthComboBox.SelectedIndex + 1);
            }
        }

        private void customersButton_Click(object sender, EventArgs e)
        {
            var customersForm = new Customers();
            customersForm.ShowDialog();
        }
    }
}
