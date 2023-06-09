﻿using C969.Controllers;
using C969.Exceptions;
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
        private bool _adding = false;
        private bool _changesMade = false;
        private bool _customerUpdated = false;
        private bool _addressUpdated = false;
        private bool _cityUpdated = false;
        private bool _countryUpdated = false;
        private readonly Customer _workingCustomer;
        private readonly Address _workingCustomerAddress;
        private readonly City _workingCustomerCity;
        private readonly Country _workingCustomerCountry;
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
        public CustomerView(CustomerController customerController, int customerId = 0, bool editing = false)
        {
            InitializeComponent();
            _editing = editing;
            if (editing)
            {
                _adding = true;
                activeCheckBox.Checked = true;
            }
            customerController.CustomerId = customerId;
            _customerController = customerController;
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
            activeCheckBox.CheckedChanged += OnCheckChanged;
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
                        switch (propertyName)
                        {
                            case "address": case "address2": case "postalCode": case "phoneNumber":
                                _addressUpdated = true;
                                break;
                            case "city":
                                _cityUpdated = true;
                                break;
                            case "country":
                                _countryUpdated = true;
                                break;
                            default:
                                _customerUpdated = true;
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Event handler for changing state of a checkbox control
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void OnCheckChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                var currentValue = checkBox.Checked;
                var propertyName = checkBox.Name.Replace("CheckBox", "");

                if (propertySetters.TryGetValue(propertyName, out var propertySetter) &&
                    propertyGetters.TryGetValue(propertyName, out var propertyGetter))
                {
                    var previousValue = propertyGetter();
                    if (checkBox.Name == "activeCheckBox")
                    {
                        if (activeCheckBox.Checked == false)
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to archive this customer? This is effectively 'deleting' them.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result != DialogResult.Yes)
                            {
                                // remove the event handler to prevent retriggering
                                activeCheckBox.CheckedChanged -= OnCheckChanged;
                                activeCheckBox.Checked = true;
                                // readd event handler
                                activeCheckBox.CheckedChanged += OnCheckChanged;
                                return;
                            }
                            WorkingCustomer.active = false;
                        }
                    }
                    if (currentValue.ToString() != previousValue)
                    {
                        ChangesMade = true;
                        propertySetter(currentValue.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Fills out the customer form fields with the information from the currently selected customer.
        /// </summary>
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
        /// Handler for the close button click event
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The EventArgs object containing the event data</param>
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
        /// Handles saving the customer and relevant details
        /// </summary>
        /// <remarks>
        /// This method prepares add and update operations and their corresponding messages
        /// Results and messages are gathered and messages displayed in message boxes
        /// </remarks>
        private void HandleSave()
        {
            // Prepare add operations and their corresponding messages
            var addOperations = new List<(Func<int> Action, string ObjectName, string SuccessMessage, string ErrorMessage)>
                {
                    (() => _customerController.HandleAddCountry(WorkingCustomerCountry), "Country", "Customer country added successfully", "Something went wrong adding the customer's country"),
                    (() => _customerController.HandleAddCity(WorkingCustomerCity), "City", "Customer city added successfully", "Something went wrong adding the customer's city"),
                    (() => _customerController.HandleAddAddress(WorkingCustomerAddress), "Address", "Customer address added successfully", "Something went wrong adding the customer's address"),
                    (() => _customerController.HandleAddCustomer(WorkingCustomer), "Customer", "Customer added successfully", "Something went wrong adding the customer")
                };

            // Prepare update operations and their corresponding messages
            var updateOperations = new List<(Func<bool> Action, string SuccessMessage, string ErrorMessage)>
                {
                    (() => _customerController.HandleUpdateCustomer(WorkingCustomer), "Customer saved successfully", "Something went wrong saving the customer"),
                    (() => _customerController.HandleUpdateAddress(WorkingCustomerAddress), "Customer address saved successfully", "Something went wrong saving the customer's address"),
                    (() => _customerController.HandleUpdateCity(WorkingCustomerCity), "Customer city saved successfully", "Something went wrong saving the customer's city"),
                    (() => _customerController.HandleUpdateCountry(WorkingCustomerCountry), "Customer country saved successfully", "Something went wrong saving the customer's country")
                };

            // Gather all the results and messages
            var results = new List<(bool Success, string Message)>();

            if (_adding)
            {
                foreach (var operation in addOperations)
                {
                    try
                    {
                        var newId = operation.Action();
                        if (newId != 0)
                        {
                            if (operation.ObjectName == "Country")
                            {
                                WorkingCustomerCity.countryId = newId;
                            }
                            else if (operation.ObjectName == "City")
                            {
                                WorkingCustomerAddress.cityId = newId;
                            }
                            else if (operation.ObjectName == "Address")
                            {
                                WorkingCustomer.addressId = newId;
                            }
                        }
                        var message = newId != 0 ? operation.SuccessMessage : operation.ErrorMessage;
                        results.Add((newId != 0, message));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                // Check which updates we need to perform
                var updatesToPerform = new List<(Func<bool> Action, string SuccessMessage, string ErrorMessage)>
                    {
                        _customerUpdated ? updateOperations[0] : default,
                        _addressUpdated ? updateOperations[1] : default,
                        _cityUpdated ? updateOperations [2] : default,
                        _countryUpdated ? updateOperations[3] : default,
                    }.Where(x => x.Action != null).ToList();
                foreach (var update in updatesToPerform)
                {
                    var result = update.Action();
                    var message = result ? update.SuccessMessage : update.ErrorMessage;
                    results.Add((result, message));
                }
            }

            var successMessages = results.Where(r => r.Success).Select(r => r.Message);
            var errorMessages = results.Where(r => !r.Success).Select(r => r.Message);


            if (successMessages.Any())
            {
                MessageBox.Show(string.Join("\n", successMessages), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }
        private void EditSaveButton_Click(object sender, EventArgs e)
        {
            if (_editing)
            {
                HandleSave();
            }
            else
            {
                HandleToggleEdit();
            }
        }

        private void countryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (_editing)
                {
                    HandleSave();
                } else
                {
                    HandleToggleEdit();
                }
            }
        }
    }
}
