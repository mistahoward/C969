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
    public partial class Reports : Form
    {
        private readonly AppointmentController _appointmentController;
        public Reports(AppointmentController appointmentController)
        {
            InitializeComponent();

            _appointmentController = appointmentController;

            reportsComboBox.Items.Add("Number of Appointment Types By Month");
            reportsComboBox.Items.Add("Schedule of Each Consultant");
            reportsComboBox.Items.Add("Appointments By Customer");
        }

        private void LoadReport1()
        {
            List<Appointment> appointments = _appointmentController.GetAllAppointments();
            var groupedAppointments = appointments
                .GroupBy(a => new { a.start.Month, a.type })
                .Select(g => new { g.Key.Month, Type = g.Key.type, Count = g.Count() })
                .ToList();

            reportDataGridView.DataSource = groupedAppointments;
        }
        private void LoadReport2()
        {
            List<Appointment> appointments = _appointmentController.GetAllAppointments();

            var userAppointments = appointments
                .OrderBy(a => a.userId)
                .ThenBy(a => a.start)
                .Select(a => new
                {
                    UserID = a.userId,
                    AppointmentID = a.appointmentId,
                    Type = a.type,
                    Start = a.start,
                    End = a.end
                })
                .ToList();

            reportDataGridView.DataSource = userAppointments;
        }

        private void LoadReport3()
        {
            List<Appointment> appointments = _appointmentController.GetAllAppointments();

            var customerAppointments = appointments
               .OrderBy(a => a.customerId)
               .ThenBy(a => a.start)
               .Select(a => new
               {
                   CustomerID = a.customerId,
                   AppointmentID = a.appointmentId,
                   Type = a.type,
                   Start = a.start,
                   End = a.end
               })
               .ToList();

            reportDataGridView.DataSource = customerAppointments;
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            string selectedReport = reportsComboBox.SelectedItem.ToString();

            switch (selectedReport)
            {
                case "Number of Appointment Types By Month":
                    // Load data for Report 1
                    LoadReport1();
                    break;
                case "Schedule of Each Consultant":
                    // Load data for Report 2
                    LoadReport2();
                    break;
                case "Appointments By Customer":
                    // Load data for Report 3
                    LoadReport3();
                    break;
                default:
                    MessageBox.Show("Please select a valid report.");
                    break;
            }
        }

        private void reportsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
