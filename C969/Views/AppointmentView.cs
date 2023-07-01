using C969.Controllers;
using C969.Models;
using C969.Utilities;
using C969.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969
{
    public partial class AppointmentView : Form
    {
        private readonly AppointmentController _appointmentController;
        private readonly CustomerController _customerController;
        private readonly Dictionary<string, Action<string>> propertySetters;
        private readonly Dictionary<string, Func<string>> propertyGetters;
        private readonly Dictionary<string, Action<DateTime>> dateTimePropertySetters;
        private readonly Dictionary<string, Func<DateTime>> dateTimePropertyGetters;
        private readonly Appointment _workingAppointment;
        private readonly Appointment _appointment;
        private List<CustomerMeta> CustomersMetaList { get; set; }
        private Customer _selectedCustomer => _customerController.Customer;
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
            _customerController = new CustomerController();
            _appointment = _appointmentController.Appointment;
            _workingAppointment = _appointmentController.Appointment;
            startDateTimePicker.Format = DateTimePickerFormat.Custom;
            startDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            endDateTimePicker.Format = DateTimePickerFormat.Custom;
            endDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            if (Appointment.customerId != 0)
            {
                _customerController.CustomerId = Appointment.customerId;
            }
            FillOutFields();
            AttachEventHandlers();
            ChangesMadeChanged += OnChangesMadeChanged;
            HandleCustomers();

            if (_editing)
            {
                HandleToggleEdit();
            } else
            {
                EditSaveButton.Text = "Edit";
            }
            // Initializing property setters and getters on appointment to allow for "change tracking" and removing the needs for verbose switch statements
            propertySetters = new Dictionary<string, Action<string>>
            {
                { "title", (value) => _workingAppointment.title = value },
                { "description", (value) => _workingAppointment.description = value },
                { "location", (value) => _workingAppointment.location = value },
                { "contact", (value) => _workingAppointment.contact = value },
                { "type", (value) => _workingAppointment.type = value },
                { "url", (value) => _workingAppointment.url = value },
            };
            propertyGetters = new Dictionary<string, Func<string>>
            {
                { "title", () => _workingAppointment.title },
                { "description", () => _workingAppointment.description },
                { "location", () => _workingAppointment.location },
                { "contact", () => _workingAppointment.contact },
                { "type", () => _workingAppointment.type },
                { "url", () => _workingAppointment.url },
                { "customerId", () => _selectedCustomer.customerId.ToString() }
            };
            dateTimePropertySetters = new Dictionary<string, Action<DateTime>>
            {
                { "start", (value) => _workingAppointment.start = value.ToUniversalTime() },
                { "end", (value) => _workingAppointment.end = value.ToUniversalTime() },
            };
            dateTimePropertyGetters = new Dictionary<string, Func<DateTime>>
            {
                { "start", () => EpochConverter.ConvertUtcToUserTime(_workingAppointment.start) },
                { "end", () => EpochConverter.ConvertUtcToUserTime(_workingAppointment.end) },
            };
        }
        private void HandleCustomers()
        {
            CustomersMetaList = new List<CustomerMeta>();
            CustomersMetaList = _customerController.Customers.Where(c => c.active)
                .Select(c =>
                new CustomerMeta
                {
                    customerId = c.customerId,
                    customerName = c.customerName,
                    active = c.active
                }).ToList();
            if (_selectedCustomer != null)
            {
                CustomersMetaList.Where(c => c.customerId != _selectedCustomer.customerId).ToList();
            }
            CustomerDataGridView.DataSource = CustomersMetaList;
        }
        private void HandleToggleEdit()
        {
            _editing = true;
            EditSaveButton.Text = "Save";
            EditSaveButton.Enabled = false;
            titleTextBox.Enabled = true;
            descriptionTextBox.Enabled = true;
            locationTextBox.Enabled = true;
            contactTextBox.Enabled = true;
            typeTextBox.Enabled = true;
            urlTextBox.Enabled = true;
            startDateTimePicker.Enabled = true;
            endDateTimePicker.Enabled = true;
            CustomerDataGridView.Enabled = true;
            CustomerDataGridView.Cursor = Cursors.Arrow;
            selectCustomerButton.Enabled = true;
            if (_selectedCustomer.customerId != 0)
            {
                removeCustomerButton.Enabled = true;
            }
        }
        /// <summary>
        /// Event handler called when the ChangesMade property is updated
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event data</param>
        private void OnChangesMadeChanged(object sender, EventArgs e)
        {
            EditSaveButton.Enabled = ChangesMade;
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
            startDateTimePicker.ValueChanged+= OnDateChange;
            endDateTimePicker.ValueChanged += OnDateChange;
        }
        private void FillOutFields()
        {
            titleTextBox.Text = Appointment.title;
            descriptionTextBox.Text = Appointment.description;
            locationTextBox.Text = Appointment.location;
            contactTextBox.Text = Appointment.contact;
            typeTextBox.Text = Appointment.type;
            urlTextBox.Text = Appointment.url;
            startDateTimePicker.Value = Appointment.start;
            endDateTimePicker.Value = Appointment.end;
            if (_selectedCustomer.customerId != 0)
            {
                selectedCustomerNameTextBox.Text = _selectedCustomer.customerName;
            }
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
        /// <summary>
        /// Handler for the Date Changed event of DateTimePicker objects.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDateChange(object sender, EventArgs e)
        {
            if (sender is DateTimePicker dateTimePicker)
            {
                var currentValue = dateTimePicker.Value;
                var propertyName = dateTimePicker.Name.Replace("DateTimePicker", "");
                if (dateTimePropertySetters.TryGetValue(propertyName, out var propertySetter) &&
                    dateTimePropertyGetters.TryGetValue(propertyName, out var propertyGetter))
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
 
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (ChangesMade && _editing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to close this customer view? You have pending changes that need saved.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result != DialogResult.OK)
                {
                    return;
                }
            }
            Close();
        }

        private void EditSaveButton_Click(object sender, EventArgs e)
        {
            if (_editing)
            {
                // do stuff
            } else
            {
                HandleToggleEdit();
            }
        }

        private void CustomerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomerDataGridView.SelectedRows.Count > 0)
            {
                string selectedCustomerId = CustomerDataGridView.SelectedRows[0].Cells["customerId"].Value.ToString();
                try
                {
                    _customerController.CustomerId = Int32.Parse(selectedCustomerId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void selectCustomerButton_Click(object sender, EventArgs e)
        {
            if (CustomerDataGridView.SelectedRows.Count > 0 && _customerController.Customer != null)
            {
                ChangesMade = true;
                _workingAppointment.customerId = _selectedCustomer.customerId;
                selectedCustomerNameTextBox.Text = _selectedCustomer.customerName;
                removeCustomerButton.Enabled = true;
            }
        }

        private void removeCustomerButton_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer.customerId != 0) 
            {
                ChangesMade = true;
                _customerController.CustomerId = 0;
                removeCustomerButton.Enabled = false;
                selectedCustomerNameTextBox.Text = "";
            }
        }
    }
}
