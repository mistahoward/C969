using C969.Controllers;
using C969.Models;
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
        public Calendar(DateTime requestedDate, ViewType requestedView)
        {
            InitializeComponent();

            _appointmentController = new AppointmentController(requestedDate);

            switch (requestedView)
            {
                case ViewType.Week:
                    Appointments = _appointmentController.WeekAppointments;
                    break;
                case ViewType.Month:
                    Appointments = _appointmentController.MonthAppointments;
                    break;
                default:
                    throw new ArgumentException("Invalid view type specified");
            }
        }
    }
}
