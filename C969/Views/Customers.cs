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
        public List<Customer> CustomersList { get; set; }
        public Customers()
        {
            InitializeComponent();

            _customerController = new CustomerController();

            CustomersList = new List<Customer>();

            InitializeCustomers();
        }
        private void InitializeCustomers()
        {
            CustomersList = _customerController.Customers;
            CustomerDataGridView.DataSource = CustomersList;
        }
    }
}
