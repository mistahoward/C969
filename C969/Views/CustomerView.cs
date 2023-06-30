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
            if (_editing)
            {
                HandleToggleEdit();
            }
            else
            {
                EditSaveButton.Text = "Edit";
            }
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
        /// Event handler for when a text box in the CustomerView is changed
        /// Updates the working customer object with the changes
        /// Evaluates if workingCustomer is different than customer
        /// </summary>
        /// <param name="sender">The text box that raised the event</param> 
        /// <param name="e">The event arguments</param> 
        private void OnTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var currentValue = textBox.Text;
                var propertyName = textBox.Name.Replace("TextBox", "");
                var property = typeof(Customer).GetProperty(propertyName).GetValue(_workingCustomer, null).ToString();

                if (currentValue != property)
                {
                    _changesMade = true;
                }

                switch (propertyName)
                {
                    case "customerName":
                        _workingCustomer.customerName = currentValue;
                        break;
                    case "active":
                        _workingCustomer.active = activeCheckBox.Checked;
                        break;
                    case "address":
                        _workingCustomerAddress.address = currentValue;
                        break;
                    case "address2":
                        _workingCustomerAddress.address2 = currentValue;
                        break;
                    case "postalCode":
                        _workingCustomerAddress.postalCode = currentValue;
                        break;
                    case "city":
                        _workingCustomerCity.city = currentValue;
                        break;
                    case "country":
                        _workingCustomerCountry.country = currentValue;
                        break;
                    case "phoneNumber":
                        _workingCustomerAddress.phone = currentValue;
                        break;
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
            customerNameTextBox.Enabled = true;
            activeCheckBox.Enabled = true;
            addressTextBox.Enabled = true;
            address2TextBox.Enabled = true;
            postalCodeTextBox.Enabled = true;
            cityTextBox.Enabled = true;
            countryTextBox.Enabled = true;
            phoneNumberTextBox.Enabled = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
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
