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
using Unity;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        User loggedInUser;
        IRepository<User> userContext;
        IRepository<Role> roleContext;
        IRepository<AssignedTo> assignedToContext;
        public CreateUser(User u)
        {
            loggedInUser = u;

            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            this.assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();

            InitializeComponent();

            List<Role> roleList = roleContext.Collection().ToList();
            cmbRole.ItemsSource = roleList;
            cmbRole.DisplayMemberPath = "Name";
            cmbRole.SelectedValuePath = "Id";
            cmbRole.SelectedItem = null;
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageSystemUsers msu = new ManageSystemUsers(loggedInUser);
            msu.Show();
        }

        private async void Create(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Equals("") || txtPassword.Equals("") || cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please enter all required fields before creating a new user");
            }
            else
            {
                User user = new User();
                AssignedTo assTo = new AssignedTo();

                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;
                user.Role = cmbRole.SelectedValue.ToString();
                assTo.Name = txtUsername.Text;

                userContext.Insert(user);
                assignedToContext.Insert(assTo);

                await userContext.Commit();
                await assignedToContext.Commit();
                MessageBox.Show("User has been successfully created");
                ManageSystemUsers msu = new ManageSystemUsers(loggedInUser);
                this.Hide();
                msu.Show();
            }
        }
    }
}
