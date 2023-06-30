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
            if (_editing)
            {
                HandleToggleEdit();
            } else
            {
                EditSaveButton.Text = "Edit";
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
            } else
            {
                HandleToggleEdit();
            }
        }
    }
}
