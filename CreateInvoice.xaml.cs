using System;
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
using AdvancedProgramming.Contracts;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using Unity;
using DatabaseExample.Models;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for CreateInvoice.xaml
    /// </summary>
    public partial class CreateInvoice : Window
    {
        User loggedInUser;

        AuditLog audit = new AuditLog();

        IRepository<Invoice> invoiceContext;
        IRepository<Customer> customerContext;
        public CreateInvoice(User u, string jobID)
        {
            loggedInUser = u;
            
            this.invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            
            InitializeComponent();
            txtJobID.Text = jobID;
            
            List<Invoice> invoiceList = invoiceContext.Collection().ToList();

            List<Customer> customerList = customerContext.Collection().ToList();
            cmbCustomerName.ItemsSource = customerList;
            cmbCustomerName.DisplayMemberPath = "Name";
            cmbCustomerName.SelectedValuePath = "Id";
            cmbCustomerName.SelectedItem = null;

        }

        public async void Create(object sender, RoutedEventArgs e)
        {
            if (txtJobID.Equals("") || cmbCustomerName.SelectedItem == null || txtAmountOwed.Equals("") || txtAmountPaid.Equals("")
               || txtPaymentSchedule.Equals("") || txtDate.Equals(""))
            {
                MessageBox.Show("Please enter all required fields before creating a new invoice");
            }
            else
            {
                Invoice invoice = new Invoice();
                invoice.JobID = txtJobID.Text;
                invoice.CustomerName = cmbCustomerName.SelectedValue.ToString();
                invoice.AmountOwed = Convert.ToDecimal(txtAmountOwed.Text);
                invoice.AmountPaid = Convert.ToDecimal(txtAmountPaid.Text);
                invoice.PaymentSchedule = txtPaymentSchedule.Text;
                invoice.Date = Convert.ToDateTime(txtDate.Text);

                invoiceContext.Insert(invoice);
                await invoiceContext.Commit();
                audit.LogAction("created a new invoice", loggedInUser.ToString());
                MessageBox.Show("Invoice has been successfully created");
                //ManageInvoices mi = new ManageInvoices(loggedInUser);
                //this.Hide();
                //mt.Show();
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            string jobID = txtJobID.Text;
            Hide();
            audit.LogAction("returned to manage invoices page", loggedInUser.ToString());
            ManageInvoices mi = new ManageInvoices(loggedInUser, jobID);
            mi.Show();
        }
    }
}
