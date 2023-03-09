using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AdvancedProgramming.Contracts;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using Unity;
using DatabaseExample.Models;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for ManageInvoices.xaml
    /// </summary>
    public partial class ManageInvoices : Window
    {
        //get the logged in user
        User loggedInUser;

        IRepository<Customer> customerContext;
        IRepository<Invoice> invoiceContext;

        //create a list for customer
        List<Customer> customersList;
        Customer selectedCustomer;
        int customerListSize = 0;
        int customerPosition = 0;

        //create a list for invoice
        List<Invoice> invoicesList;
        Invoice selectedInvoice;
        int invoiceListSize = 0;
        int invoicePosition = 0;
        public ManageInvoices(User u, string jobID)
        {
            loggedInUser = u;

            this.invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();

            InitializeComponent();
            RefreshData(jobID);
        }

        private void RefreshData(string jobID)
        {

            List<Customer> customerList = customerContext.Collection().ToList();
            cmbCustomerName.ItemsSource = customerList;
            cmbCustomerName.DisplayMemberPath = "Name";
            cmbCustomerName.SelectedValuePath = "Id";

            customersList = customerContext.Collection().ToList();
            customerListSize = customersList.Count();

            selectedCustomer = customersList.FirstOrDefault();
            customerPosition = customersList.IndexOf(selectedCustomer);


            List<Invoice> invoiceList = invoiceContext.Collection().ToList();

            invoicesList = invoiceContext.Collection().ToList();
            invoiceListSize = invoicesList.Count();

            selectedInvoice = invoicesList.FirstOrDefault();
            invoicePosition = invoicesList.IndexOf(selectedInvoice);

            //set values of fields 
            txtJobID.Text = jobID;
            cmbCustomerName.SelectedValue = selectedInvoice.CustomerName;
            txtAmountOwed.Text = selectedInvoice.AmountOwed.ToString();
            txtAmountPaid.Text = selectedInvoice.AmountPaid.ToString();
            txtPaymentSchedule.Text = selectedInvoice.PaymentSchedule;
            txtDate.Text = selectedInvoice.Date.ToString();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            string jobID = txtJobID.Text;
            this.Hide();
            ManageTasks mt = new ManageTasks(loggedInUser, jobID);
            mt.Show();
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            selectedInvoice.JobID = txtJobID.Text;
            selectedInvoice.CustomerName = cmbCustomerName.SelectedValue.ToString();
            selectedInvoice.AmountOwed = Convert.ToDecimal(txtAmountOwed.Text);
            selectedInvoice.AmountPaid = Convert.ToDecimal(txtAmountPaid.Text);
            selectedInvoice.PaymentSchedule = txtPaymentSchedule.Text;
            selectedInvoice.Date = Convert.ToDateTime(txtDate.Text);

            invoiceContext.Update(selectedInvoice);
            await invoiceContext.Commit();
            MessageBox.Show("Record saved successfully!");
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            selectedCustomer = customersList.FirstOrDefault();
            //selectedUser = usersList.FirstOrDefault();
            //selectedJob = jobsList.FirstOrDefault();
            //selectedAssignedTo = assignedTosList.FirstOrDefault();
            //selectedCompleted = completedsList.FirstOrDefault();

            customerPosition = customersList.IndexOf(selectedCustomer);
            //userPosition = usersList.IndexOf(selectedUser);
            //jobPosition = jobsList.IndexOf(selectedJob);

            cmbCustomerName.SelectedValue = selectedInvoice.CustomerName;
            txtAmountOwed.Text = selectedInvoice.AmountOwed.ToString();
            txtAmountPaid.Text = selectedInvoice.AmountPaid.ToString();
            txtPaymentSchedule.Text = selectedInvoice.PaymentSchedule;
            txtDate.Text = selectedInvoice.Date.ToString();
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (invoicePosition != 0)
            {
                selectedInvoice = invoicesList[invoicePosition - 1];
                invoicePosition = invoicesList.IndexOf(selectedInvoice);

                cmbCustomerName.SelectedValue = selectedInvoice.CustomerName;
                txtAmountOwed.Text = selectedInvoice.AmountOwed.ToString();
                txtAmountPaid.Text = selectedInvoice.AmountPaid.ToString();
                txtPaymentSchedule.Text = selectedInvoice.PaymentSchedule;
                txtDate.Text = selectedInvoice.Date.ToString();
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (invoicePosition != invoiceListSize - 1)
            {
                invoicePosition = invoiceListSize - 1;
                selectedInvoice = invoicesList[invoicePosition];

                cmbCustomerName.SelectedValue = selectedInvoice.CustomerName;
                txtAmountOwed.Text = selectedInvoice.AmountOwed.ToString();
                txtAmountPaid.Text = selectedInvoice.AmountPaid.ToString();
                txtPaymentSchedule.Text = selectedInvoice.PaymentSchedule;
                txtDate.Text = selectedInvoice.Date.ToString();
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (invoicePosition != invoiceListSize - 1)
            {
                invoicePosition++;
                selectedInvoice = invoicesList[invoicePosition];

                cmbCustomerName.SelectedValue = selectedInvoice.CustomerName;
                txtAmountOwed.Text = selectedInvoice.AmountOwed.ToString();
                txtAmountPaid.Text = selectedInvoice.AmountPaid.ToString();
                txtPaymentSchedule.Text = selectedInvoice.PaymentSchedule;
                txtDate.Text = selectedInvoice.Date.ToString();
            }
        }
    }
}
