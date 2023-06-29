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
        public List<CustomerMeta> CustomersList { get; set; }
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
    }
}
