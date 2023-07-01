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
    public partial class AppointmentView : Form
    {
        private readonly AppointmentController _appointmentController;
        private readonly Dictionary<string, Action<string>> propertySetters;
        private readonly Dictionary<string, Func<string>> propertyGetters;
        private readonly Appointment _workingAppointment;
        private readonly Appointment _appointment;
        private Customer _selectedCustomer;
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
            FillOutFields();
            AttachEventHandlers();
            propertySetters = new Dictionary<string, Action<string>>
            {
                { "title", (value) => _workingAppointment.title = value },
                { "description", (value) => _workingAppointment.description = value },
                { "location", (value) => _workingAppointment.location = value },
                { "contact", (value) => _workingAppointment.contact = value },
                { "type", (value) => _workingAppointment.type = value },
                { "url", (value) => _workingAppointment.url = value },
                { "start", (value) => _workingAppointment.start = EpochConverter.ConvertUtcToUserTime((DateTime.Parse(value)))},
                { "end", (value) => _workingAppointment.end = EpochConverter.ConvertUtcToUserTime((DateTime.Parse(value)))},
            };
            propertyGetters = new Dictionary<string, Func<string>>
            {
                { "title", () => _workingAppointment.title },
                { "description", () => _workingAppointment.description },
                { "location", () => _workingAppointment.location },
                { "contact", () => _workingAppointment.contact },
                { "type", () => _workingAppointment.type },
                { "url", () => _workingAppointment.url },
                { "start", () => _workingAppointment.start.ToString() },
                { "end", () => _workingAppointment.end.ToString() },
                { "customerId", () => _selectedCustomer.customerId.ToString() }
            };
        }
        /// <summary>
        /// Attach event handlers to AppointmentView's text boxes tracking user's changes
        /// </summary>
        private void AttachEventHandlers()
        {
            titleTextBox.TextChanged += OnTextChange;
            descriptionTextBox.TextChanged += OnTextChange;
            locationTextBox.TextChanged += OnTextChange;
            contactTextBox.TextChanged += OnTextChange;
            typeTextBox.TextChanged += OnTextChange;
            urlTextBox.TextChanged += OnTextChange;
        }
        private void FillOutFields()
        {
            titleTextBox.Text = Appointment.title;
            descriptionTextBox.Text = Appointment.description;
            locationTextBox.Text = Appointment.location;
            contactTextBox.Text = Appointment.contact;
            typeTextBox.Text = Appointment.type;
            urlTextBox.Text = Appointment.url;
        }
        /// <summary>
        /// Handles the TextChanged event and updates the appropriate appointment property if its TextBox is changed
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event arguments</param>
        private void OnTextChange(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var currentValue = textBox.Text;
                var propertyName = textBox.Name.Replace("TextBox", "");
                if (propertySetters.TryGetValue(propertyName, out var propertySetter) &&
                propertyGetters.TryGetValue(propertyName, out var propertyGetter))
                {
                    var previousValue = propertyGetter();
                    if (currentValue != previousValue)
                    {
                        ChangesMade = true;
                        propertySetter(currentValue);
                    }
                }
            }
        }
    }
}
