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
        public Customer Customer;
        public Address CustomerAddress;
        public City CustomerCity;
        public Country CustomerCountry;
        public CustomerView(int customerId)
        {
            InitializeComponent();
            _customerController = new CustomerController
            {
                CustomerId = customerId
            };
            Customer = _customerController.Customer;
            CustomerAddress = _customerController.CustomerAddress;
            CustomerCity = _customerController.CustomerCity;
            CustomerCountry = _customerController.CustomerCountry;
            FillOutFields();
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
