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

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for ManageSystemUsers.xaml
    /// </summary>
    public partial class ManageSystemUsers : Window
    {
        IRepository<User> userContext;
        IRepository<Role> roleContext;
        User loggedInUser;

        List<User> usersList;
        User selectedUser;
        int userListsSize = 0;
        int position = 0;

        public ManageSystemUsers(User u)
        {
            loggedInUser = u;

            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            
            InitializeComponent();

            RefreshData();
        }

        private void RefreshData()
        {
            List<Role> roleList = roleContext.Collection().ToList();
            cmbRole.ItemsSource = roleList;
            cmbRole.DisplayMemberPath = "Name";
            cmbRole.SelectedValuePath = "Id";

            usersList = userContext.Collection().ToList();
            userListsSize = usersList.Count();

            selectedUser = usersList.FirstOrDefault();
            position = usersList.IndexOf(selectedUser);
            txtUsername.Text = selectedUser.Username;
            txtPassword.Text = selectedUser.Password;
            cmbRole.SelectedValue = selectedUser.Role;
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            selectedUser = usersList.FirstOrDefault();
            position = usersList.IndexOf(selectedUser);
            txtUsername.Text = selectedUser.Username;
            txtPassword.Text = selectedUser.Password;
            cmbRole.SelectedValue = selectedUser.Role;
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (position != 0)
            {
                selectedUser = usersList[position - 1];
                position = usersList.IndexOf(selectedUser);
                txtUsername.Text = selectedUser.Username;
                txtPassword.Text = selectedUser.Password;
                cmbRole.SelectedValue = selectedUser.Role;
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (position != userListsSize -1)
            {
                position = userListsSize - 1;
                selectedUser = usersList[position];
                txtUsername.Text = selectedUser.Username;
                txtPassword.Text = selectedUser.Password;
                cmbRole.SelectedValue = selectedUser.Role;
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (position != userListsSize - 1)
            {
                position++;
                selectedUser = usersList[position];
                txtUsername.Text = selectedUser.Username;
                txtPassword.Text = selectedUser.Password;
                cmbRole.SelectedValue = selectedUser.Role;
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            selectedUser.Username = txtUsername.Text;
            selectedUser.Password = txtPassword.Text;
            selectedUser.Role = cmbRole.SelectedValue.ToString();
            userContext.Update(selectedUser);
            await userContext.Commit();
            MessageBox.Show("Record saved successfully!");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SystemAdminMenu sam = new SystemAdminMenu(loggedInUser);
            sam.Show();
        }

        private async void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                userContext.Delete(selectedUser.Id);
                await userContext.Commit();
                MessageBox.Show("User has been successfully deleted!");
                RefreshData();
            }
        }

        private void NewUser(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateUser cu = new CreateUser(loggedInUser);
            cu.Show();
        }
    }
}
