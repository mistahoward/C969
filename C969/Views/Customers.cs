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

namespace C969
{
    public partial class Customers : Form
    {
        private readonly CustomerController _customerController;
        private int _selectedCustomerId;
        public List<CustomerMeta> CustomersList { get; set; }
        public int SelectedCustomerId => _selectedCustomerId;

        public Customers()
        {
            InitializeComponent();

            _customerController = new CustomerController();

            CustomersList = new List<CustomerMeta>();

            InitializeCustomers();
        }
        private void InitializeCustomers()
        {
            var workingCustomers = _customerController.Customers;
            // lambda here to convert a full customer to a meta customer - do this for much shorter and more concise code
            CustomersList = workingCustomers.Select(c =>
            new CustomerMeta {
                customerId = c.customerId,
                customerName = c.customerName,
                active = c.active
            }).ToList();
            CustomerDataGridView.DataSource = CustomersList;
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
    }
}
