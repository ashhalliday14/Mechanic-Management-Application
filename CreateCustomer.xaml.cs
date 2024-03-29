﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using AdvancedProgramming.Contracts;
using Unity;
using DatabaseExample.Models;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for CreateCustomer.xaml
    /// </summary>
    public partial class CreateCustomer : Window
    {
        User loggedInUser;
        AuditLog audit = new AuditLog();

        IRepository<Customer> customerContext;
        public CreateCustomer(User u)
        {
            loggedInUser = u;

            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();

            InitializeComponent();
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            audit.LogAction("returned to manage customers page", loggedInUser.ToString());
            ManageCustomers mc = new ManageCustomers(loggedInUser);
            mc.Show();
        }

        private async void Create(object sender, RoutedEventArgs e)
        {
            if (txtCustomerName.Equals("") || txtCustomerAddress.Equals("") || txtPhoneNumber.Equals("") || txtEmail.Equals(""))
            {
                MessageBox.Show("Please enter all required fields before creating a new customer");
            }
            else
            {
                Customer customer = new Customer();
                customer.Name = txtCustomerName.Text;
                customer.Address = txtCustomerAddress.Text;
                customer.PhoneNumber = txtPhoneNumber.Text;
                customer.Email = txtEmail.Text;
                customerContext.Insert(customer);
                await customerContext.Commit();
                MessageBox.Show("Customer has been successfully created");
                audit.LogAction("created a new customer", loggedInUser.ToString());
                ManageCustomers mc = new ManageCustomers(loggedInUser);
                this.Hide();
                mc.Show();
            }
        }
    }
}
