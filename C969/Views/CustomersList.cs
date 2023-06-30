using C969.Controllers;
using C969.Models;
using C969.Views;
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
    public partial class CustomersList : Form
    {
        private readonly CustomerController _customerController;
        private int _selectedCustomerId;
        public List<CustomerMeta> CustomersMetaList { get; set; }
        public CustomerFilters SelectedFilter { get; set; }
        public int SelectedCustomerId => _selectedCustomerId;

        public CustomersList(CustomerFilters selectedFilter = CustomerFilters.Active)
        {
            InitializeComponent();

            _customerController = new CustomerController();

            CustomersMetaList = new List<CustomerMeta>();

            InitializeCustomers();
            if (selectedFilter == CustomerFilters.Active)
            {
                activeInactiveToggle.Checked = true;
                activeInactiveToggle.Text = "Active";
            }
            Activated += new EventHandler(CustomersList_Activated);
            SelectedFilter = selectedFilter;
        }
        /// <summary>
        /// Filters customer view based on active status
        /// </summary>
        /// <param name="workingCustomers">List of Customer objects to filter</param>
        /// <param name="activeStatus">Filter for active customers if true; inactive if false</param>
        private void FilterCustomerView(List<Customer> workingCustomers, bool activeStatus)
        {
            // lambda here to convert a full customer to a meta customer - do this for much shorter and more concise code, allowing us to filter off of active as well
            CustomersMetaList = workingCustomers
                .Where(c => c.active == activeStatus)
                .Select(c =>
                new CustomerMeta
                {
                    customerId = c.customerId,
                    customerName = c.customerName,
                    active = c.active
                }).ToList();
            activeInactiveToggle.Checked = activeStatus;
            if (activeStatus)
            {
                activeInactiveToggle.Text = "Active";
                DeleteCustomerButton.Text = "Archive Customer";
            }
            else
            {
                activeInactiveToggle.Text = "Inactive";
                DeleteCustomerButton.Text = "Activate Customer";
            }
        }
        ///<summary>
        ///Initializes the customer list based on active status selected by user
        ///</summary>
        private void InitializeCustomers()
        {
            var workingCustomers = _customerController.Customers;
            bool active = true;
            if (SelectedFilter == CustomerFilters.Inactive)
            {
                active = false;
            }
            FilterCustomerView(workingCustomers, active);

            CustomerDataGridView.DataSource = CustomersMetaList;
        }
        /// <summary>
        /// Handle selection changed event of CustomerDataGridView and set selected customer
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void CustomerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomerDataGridView.SelectedRows.Count > 0)
            {
                string selectedCustomerId = CustomerDataGridView.SelectedRows[0].Cells["customerId"].Value.ToString();
                try
                {
                    _selectedCustomerId = Int32.Parse(selectedCustomerId);
                    _customerController.CustomerId = _selectedCustomerId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void ViewCustomerButton_Click(object sender, EventArgs e)
        {
            if (CustomerDataGridView.SelectedRows.Count < 0)
            {
                MessageBox.Show("Please select a customer before attempting to view one", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            var viewCustomer = new CustomerView(_customerController, customerId: SelectedCustomerId);
            viewCustomer.ShowDialog();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomersList_Activated(object sender, EventArgs e)
        {
            InitializeCustomers();
        }

        private void activeInactiveToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (activeInactiveToggle.Checked)
            {
                SelectedFilter = CustomerFilters.Active;
                InitializeCustomers();
            } else
            {
                SelectedFilter = CustomerFilters.Inactive;
                InitializeCustomers();
            }
        }

        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            if (CustomerDataGridView.SelectedRows.Count < 0)
            {
                string message = (SelectedFilter == CustomerFilters.Active) ?
                    "Please select a customer before attempting to archive one" :
                    "Please select a customer before attempting to reactivate one";
                MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string successMessage = (SelectedFilter == CustomerFilters.Active) ?
                "Customer archived successfully" :
                "Customer reactived successfully";
            string errorMessage = (SelectedFilter == CustomerFilters.Active) ?
                "Customer failed to archive" :
                "Customer failed to reactive";
            if (SelectedFilter == CustomerFilters.Active)
            {
                if (ConfirmAction("Are you sure you want to archive this customer?", "Confirm Archive"))
                {
                   var result = ChangeCustomerStatus(false);
                    MessageBox.Show(result ? successMessage : errorMessage, result ? "Success" : "Error", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
            }
            else if (SelectedFilter == CustomerFilters.Inactive)
            {
                if (ConfirmAction("Are you sure you want to reactivate this customer?", "Confirm Reactivation"))
                {
                    var result = ChangeCustomerStatus(true);
                    MessageBox.Show(result ? successMessage : errorMessage, result ? "Success" : "Error", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
            }
        }

        private bool ConfirmAction(string message, string title)
        {
            DialogResult response = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return response == DialogResult.Yes;
        }

        private bool ChangeCustomerStatus(bool status)
        {
            Customer updatedCustomer = _customerController.Customer;
            updatedCustomer.active = status;
            var result = _customerController.HandleUpdateCustomer(updatedCustomer);
            InitializeCustomers();
            return result;
        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            var addCustomer = new CustomerView(_customerController, editing:true);
            addCustomer.ShowDialog();
        }
    }
}
