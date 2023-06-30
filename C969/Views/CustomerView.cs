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
        /// </summary>
        /// <param name="sender">The text box that raised the event</param> 
        /// <param name="e">The event arguments</param> 
        private void OnTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (textBox.Name)
                {
                    case "customerNameTextBox":
                        _workingCustomer.customerName = textBox.Text;
                        break;
                    case "activeCheckBox":
                        _workingCustomer.active = activeCheckBox.Checked;
                        break;
                    case "addressTextBox":
                        _workingCustomerAddress.address = addressTextBox.Text;
                        break;
                    case "address2TextBox":
                        _workingCustomerAddress.address2 = address2TextBox.Text;
                        break;
                    case "postalCodeTextBox":
                        _workingCustomerAddress.postalCode = postalCodeTextBox.Text;
                        break;
                    case "cityTextBox":
                        _workingCustomerCity.city = cityTextBox.Text;
                        break;
                    case "countryTextBox":
                        _workingCustomerCountry.country = countryTextBox.Text;
                        break;
                    case "phoneNumberTextBox":
                        _workingCustomerAddress.phone = phoneNumberTextBox.Text;
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
