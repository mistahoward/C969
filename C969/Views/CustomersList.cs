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
        private void FilterCustomerView(List<Customer> workingCustomers, bool active)
        {
            // lambda here to convert a full customer to a meta customer - do this for much shorter and more concise code, allowing us to filter off of active as well
            CustomersMetaList = workingCustomers.Select(c =>
            new CustomerMeta
            {
                customerId = c.customerId,
                customerName = c.customerName,
                active = c.active
            }).Where(c => c.active = active).ToList();
            activeInactiveToggle.Checked = active;
            if (active)
            {
                activeInactiveToggle.Text = "Active";
            } else
            {
                activeInactiveToggle.Text = "Inactive";
            }
        }
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
        /// Handle selection changed event of CustomerDataGridView and set selected customer.
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
            var viewCustomer = new CustomerView(SelectedCustomerId, _customerController);
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
    }
}
