using C969.Controllers;
using C969.Exceptions;
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
        /// <summary>
        /// AppointmentView constructor
        /// </summary>
        /// <param name="appointmentController">An instance of AppointmentController</param>
        /// <param name="appointmentId">Appointment id (optional, defaults to 0)</param>
        /// <param name="editing">If true, start the view in edit mode</param>
        public AppointmentView(AppointmentController appointmentController, int appointmentId = 0, bool editing = false)
        {
            InitializeComponent();
            _editing = editing;
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
                _adding = true;
                HandleToggleEdit();
            }
            else
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
                { "start", (value) => _workingAppointment.start = value },
                { "end", (value) => _workingAppointment.end = value },
            };
            dateTimePropertyGetters = new Dictionary<string, Func<DateTime>>
            {
                { "start", () => DateTime.SpecifyKind(EpochConverter.ConvertUtcToUserTime(_workingAppointment.start), DateTimeKind.Local) },
                { "end", () => DateTime.SpecifyKind(EpochConverter.ConvertUtcToUserTime(_workingAppointment.end), DateTimeKind.Local) },
            };
        }
        /// <summary>
        /// Populates the CustomerDataGridView with active customer data and sets the DataSource as CustomersMetaList. If a customer is already selected, filters out its data.
        /// </summary>
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
        /// <summary>
        /// Handles toggling editing mode for the appointment view
        /// </summary>
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
        /// <summary>
        /// Fills out the fields of the appointment view based on the appointment data.
        /// </summary>
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
 
       /// <summary>
        /// Handles the Click event of the closeButton control. Closes the form after validating if there are any unsaved changes
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The EventArgs instance containing the event data</param>
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

        /// <summary>
        /// Handles the selection change event for the customer data grid view
        /// </summary>
        /// <param name="sender">The object that raised the selection changed event</param>
        /// <param name="e">The event data.</param>
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
        /// <summary>
        /// Handle selecting a customer from the DataGridView and updating the appointment 
        /// with the newly selected Customer
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The EventArgs associated with the event</param>
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
        /// <summary>
        /// Handles the click event for the Remove Customer button.
        /// Removes the selected customer from the appointment. 
        /// </summary>
        /// <param name="sender">The button that triggered the event</param>
        /// <param name="e">The event arguments</param>
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
        private void EditSaveButton_Click(object sender, EventArgs e)
        {
            bool cancelClose = false;
            if (_workingAppointment.customerId == 0)
            {
                MessageBox.Show("Please add a customer before continuing", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                bool result;
                if (_adding)
                {
                    result = _appointmentController.HandleAddAppointment(_workingAppointment);
                }
                else if (_editing)
                {
                    result = _appointmentController.HandleUpdateAppointment(_workingAppointment);
                }
                else
                {
                    throw new InvalidOperationException("Neither adding nor editing is currently active");
                }

                if (result)
                {
                    MessageBox.Show("Appointment Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Appointment failed to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidObject)
            {
                cancelClose = true;
                MessageBox.Show("Appointment is missing required data - please fill out all fields before proceeding", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                cancelClose = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!cancelClose)
            {
                Close();
            }
        }
    }
}
