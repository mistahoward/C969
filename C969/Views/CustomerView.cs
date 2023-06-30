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

namespace C969.Views
{
    public partial class CustomerView : Form
    {
        private readonly CustomerController _customerController;
        private readonly Dictionary<string, Action<string>> propertySetters;
        private readonly Dictionary<string, Func<string>> propertyGetters;
        private bool _editing = false;
        private bool _changesMade = false;
        private Customer _workingCustomer;
        private Address _workingCustomerAddress;
        private City _workingCustomerCity;
        private Country _workingCustomerCountry;
        private readonly Customer _customer;
        private readonly Address _customerAddress;
        private readonly City _customerCity;
        private readonly Country _customerCountry;
        public Customer WorkingCustomer => _workingCustomer;
        public Address WorkingCustomerAddress => _workingCustomerAddress;
        public City WorkingCustomerCity => _workingCustomerCity;
        public Country WorkingCustomerCountry => _workingCustomerCountry;
        public Customer Customer => _customer;
        public Address CustomerAddress => _customerAddress;
        public City CustomerCity => _customerCity;
        public Country CustomerCountry => _customerCountry;
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
        /// Initializes a new instance of the CustomerView class
        /// </summary>
        /// <param name="customerId">The ID of the customer to display</param>
        public CustomerView(int customerId)
        {
            InitializeComponent();
            _customerController = new CustomerController
            {
                CustomerId = customerId
            };
            _customer = _customerController.Customer;
            _customerAddress = _customerController.CustomerAddress;
            _customerCity = _customerController.CustomerCity;
            _customerCountry = _customerController.CustomerCountry;
            _workingCustomer = _customerController.Customer;
            _workingCustomerAddress = _customerController.CustomerAddress;
            _workingCustomerCity = _customerController.CustomerCity;
            _workingCustomerCountry = _customerController.CustomerCountry;
            FillOutFields();
            AttachEventHandlers();
            ChangesMadeChanged += OnChangesMadeChanged;
            if (_editing)
            {
                HandleToggleEdit();
            }
            else
            {
                EditSaveButton.Text = "Edit";
            }
            // Initializing property setters and getters on workingCustomer to allow for "change tracking" and removing the needs for verbose switch statements
            propertySetters = new Dictionary<string, Action<string>>
            {
                { "customerName", (value) => _workingCustomer.customerName = value },
                { "active", (value) => _workingCustomer.active = value == "1" },
                { "address", (value) => _workingCustomerAddress.address = value },
                { "address2", (value) => _workingCustomerAddress.address2 = value },
                { "postalCode", (value) => _workingCustomerAddress.postalCode = value },
                { "city", (value) => _workingCustomerCity.city = value },
                { "country", (value) => _workingCustomerCountry.country = value },
                { "phoneNumber", (value) => _workingCustomerAddress.phone = value }
            };

            propertyGetters = new Dictionary<string, Func<string>>
            {
                { "customerName", () => _workingCustomer.customerName },
                { "active", () => _workingCustomer.active ? "1" : "0" },
                { "address", () => _workingCustomerAddress.address },
                { "address2", () => _workingCustomerAddress.address2 },
                { "postalCode", () => _workingCustomerAddress.postalCode },
                { "city", () => _workingCustomerCity.city },
                { "country", () => _workingCustomerCountry.country },
                { "phoneNumber", () => _workingCustomerAddress.phone }
            };
        }
        /// <summary>
        /// Attach event handlers to CustomerView's text boxes tracking user's changes
        /// </summary>
        private void AttachEventHandlers()
        {
            customerNameTextBox.TextChanged += OnTextChanged;
            addressTextBox.TextChanged += OnTextChanged;
            address2TextBox.TextChanged += OnTextChanged;
            postalCodeTextBox.TextChanged += OnTextChanged;
            cityTextBox.TextChanged += OnTextChanged;
            countryTextBox.TextChanged += OnTextChanged;
            phoneNumberTextBox.TextChanged += OnTextChanged;
        }
        /// <summary>
        /// Handles the TextChanged event and updates the appropriate Customer property if its TextBox is changed
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event arguments</param>
        private void OnTextChanged(object sender, EventArgs e)
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
        private void FillOutFields()
        {
            customerNameTextBox.Text = Customer.customerName;
            activeCheckBox.Checked = Customer.active;
            addressTextBox.Text = CustomerAddress.address;
            address2TextBox.Text = CustomerAddress.address2;
            postalCodeTextBox.Text = CustomerAddress.postalCode;
            cityTextBox.Text = CustomerCity.city;
            countryTextBox.Text = CustomerCountry.country;
            phoneNumberTextBox.Text = CustomerAddress.phone;
        }
        /// <summary> 
        /// Enables editing of customer details
        /// <remarks>
        /// Changes the "Save/Edit" button text and enabled all customer detail fields for editing
        /// </remarks>
        /// </summary>
        private void HandleToggleEdit()
        {
            _editing = true;
            EditSaveButton.Text = "Save";
            EditSaveButton.Enabled = false;
            customerNameTextBox.Enabled = true;
            activeCheckBox.Enabled = true;
            addressTextBox.Enabled = true;
            address2TextBox.Enabled = true;
            postalCodeTextBox.Enabled = true;
            cityTextBox.Enabled = true;
            countryTextBox.Enabled = true;
            phoneNumberTextBox.Enabled = true;
        }
        private void OnChangesMadeChanged(object sender, EventArgs e)
        {
            EditSaveButton.Enabled = ChangesMade;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (ChangesMade && _editing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to close this customer view? You have pending changes that need saved.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result != DialogResult.OK) {
                    return;
                }
            }
            Close();
        }

        private void EditSaveButton_Click(object sender, EventArgs e)
        {
            if (_editing)
            {
                // save customer function
            }
            else
            {
                HandleToggleEdit();
            }
        }
    }
}
