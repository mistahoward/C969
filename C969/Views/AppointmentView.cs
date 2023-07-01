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
    public partial class AppointmentView : Form
    {
        private readonly AppointmentController _appointmentController;
        private readonly Appointment _workingAppointment;
        private readonly Appointment _appointment;
        private readonly Customer _selectedCustomer;
        private bool _editing = false;
        private bool _adding = false;
        private bool _changesMade = false;
        public Appointment Appointment => _appointment;
        public Customer Customer => _selectedCustomer;
        public event EventHandler ChangesMadeChanged;
        /// <summary>
        /// Gets or sets a value indicating whether changes have been made.
        /// </summary>
        /// <value>
        ///   <c>true</c> if changes have been made; otherwise, <c>false</c>.
        /// </value>
        public bool ChangesMade
        {
            get { return _changesMade; }
            /// <summary>
            /// Sets the value indicating whether changes have been made and triggers an event if it has changed
            /// </summary>
            /// <value>
            ///   <c>true</c> if changes have been made; otherwise, <c>false</c>.
            /// </value>
            /// <remarks>
            /// Adding changes that have been made to the form.
            /// </remarks>
            set
            {
                if (_changesMade != value)
                {
                    _changesMade = value;
                    ChangesMadeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public AppointmentView(AppointmentController appointmentController, int appointmentId = 0, bool editing = false)
        {
            InitializeComponent();
            _editing = editing;
            if (editing)
            {
                _adding = true;
            }
            appointmentController.AppointmentId = appointmentId;
            _appointmentController = appointmentController;
            _appointment = _appointmentController.Appointment;
            _workingAppointment = _appointmentController.Appointment;
        }
    }
}
