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
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using AdvancedProgramming.Contracts;
using Unity;
using DatabaseExample.Models;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for OfficeAdminManageCustomers.xaml
    /// </summary>
    public partial class OfficeAdminManageCustomers : Window
    {
        IRepository<Customer> customerContext;

        User loggedInUser;

        AuditLog audit = new AuditLog();

        List<Customer> customersList;
        Customer selectedCustomer;
        int customerListsSize = 0;
        int position = 0;
        public OfficeAdminManageCustomers(User u)
        {
            loggedInUser = u;
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();

            InitializeComponent();

            RefreshData();
        }

        private void RefreshData()
        {
            customersList = customerContext.Collection().ToList();
            customerListsSize = customersList.Count();

            selectedCustomer = customersList.FirstOrDefault();
            position = customersList.IndexOf(selectedCustomer);
            txtCustomerName.Text = selectedCustomer.Name;
            txtCustomerAddress.Text = selectedCustomer.Address;
            txtPhoneNumber.Text = selectedCustomer.PhoneNumber;
            txtEmail.Text = selectedCustomer.Email;
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            audit.LogAction("clicked to view first customer", loggedInUser.ToString());
            selectedCustomer = customersList.FirstOrDefault();
            position = customersList.IndexOf(selectedCustomer);
            txtCustomerName.Text = selectedCustomer.Name;
            txtCustomerAddress.Text = selectedCustomer.Address;
            txtPhoneNumber.Text = selectedCustomer.PhoneNumber;
            txtEmail.Text = selectedCustomer.Email;
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (position != 0)
            {
                audit.LogAction("clicked to view previous customer", loggedInUser.ToString());
                selectedCustomer = customersList[position - 1];
                position = customersList.IndexOf(selectedCustomer);
                txtCustomerName.Text = selectedCustomer.Name;
                txtCustomerAddress.Text = selectedCustomer.Address;
                txtPhoneNumber.Text = selectedCustomer.PhoneNumber;
                txtEmail.Text = selectedCustomer.Email;
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (position != customerListsSize - 1)
            {
                audit.LogAction("clicked to view next customer", loggedInUser.ToString());
                position = customerListsSize - 1;
                selectedCustomer = customersList[position];
                txtCustomerName.Text = selectedCustomer.Name;
                txtCustomerAddress.Text = selectedCustomer.Address;
                txtPhoneNumber.Text = selectedCustomer.PhoneNumber;
                txtEmail.Text = selectedCustomer.Email;
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (position != customerListsSize - 1)
            {
                audit.LogAction("clicked to view last customer", loggedInUser.ToString());
                position++;
                selectedCustomer = customersList[position];
                txtCustomerName.Text = selectedCustomer.Name;
                txtCustomerAddress.Text = selectedCustomer.Address;
                txtPhoneNumber.Text = selectedCustomer.PhoneNumber;
                txtEmail.Text = selectedCustomer.Email;
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            selectedCustomer.Name = txtCustomerName.Text;
            selectedCustomer.Address = txtCustomerAddress.Text;
            selectedCustomer.PhoneNumber = txtPhoneNumber.Text;
            selectedCustomer.Email = txtEmail.Text;
            customerContext.Update(selectedCustomer);
            await customerContext.Commit();
            MessageBox.Show("Record saved successfully!");
            audit.LogAction("updated a customer record", loggedInUser.ToString());
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OfficeAdminMenu oam = new OfficeAdminMenu(loggedInUser);
            audit.LogAction("entered office admin menu", loggedInUser.ToString());
            oam.Show();
        }

        private async void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                customerContext.Delete(selectedCustomer.Id);
                await customerContext.Commit();
                MessageBox.Show("Customer has been successfully deleted!");
                audit.LogAction("deleted a customer", loggedInUser.ToString());
                RefreshData();
            }
        }

        private void CreateCustomer(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OfficeAdminCreateCustomers oicc = new OfficeAdminCreateCustomers(loggedInUser);
            audit.LogAction("entered create a customer page", loggedInUser.ToString());
            oicc.Show();
        }
    }
}
