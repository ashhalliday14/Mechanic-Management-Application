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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using AdvancedProgramming.Contracts;
using Unity;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository<User> userContext;
        IRepository<Role> roleContext;
        List<Role> roleList;
        string roleSystemAdmin, roleHeadMechanic, roleMechanic, roleOfficeAdmin;
        
        public MainWindow()
        {
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();

            SetRoles();

            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            List<User> users = userContext.Collection().ToList();
            User loggedInUser;

            foreach (User u in users)
            {
                if (u.Username.Equals(usernameTextBox.Text) && u.Password.Equals(passwordTextBox.Password))
                {
                    loggedInUser = u;

                    if (loggedInUser.Role.Equals(roleSystemAdmin))
                    {
                        this.Hide();
                        MessageBox.Show("Welcome Sys Adm");
                        SystemAdminMenu sam = new SystemAdminMenu(loggedInUser);
                        sam.Show();
                    }
                    if (loggedInUser.Role.Equals(roleHeadMechanic))
                    {
                        this.Hide();
                        MessageBox.Show("Welcome Head mec");
                        //HeadMechanicMenu hmm = new HeadMechanicMenu(loggedInUser);
                        //hmm.Show();
                    }
                    if (loggedInUser.Role.Equals(roleMechanic))
                    {
                        this.Hide();
                        MessageBox.Show("Welcome mec");
                        //MechanicMenu mm = new MechanicMenu(loggedInUser);
                        //mm.Show();
                    }
                    if (loggedInUser.Role.Equals(roleOfficeAdmin))
                    {
                        this.Hide();
                        MessageBox.Show("Welcome off Adm");
                        //OfficeAdminMenu oam = new OfficeAdminMenu(loggedInUser);
                        //oam.Show();
                    }
                }
            }
        }

        private void SetRoles()
        {
            roleList = roleContext.Collection().ToList();
            
            foreach (Role r in roleList)
            {
                if (r.Name.Equals("System Admin"))
                {
                    roleSystemAdmin = r.Id;
                }
                if (r.Name.Equals("Head Mechanic"))
                {
                    roleHeadMechanic = r.Id;
                }
                if (r.Name.Equals("Mechanic"))
                {
                    roleMechanic = r.Id;
                }
                if (r.Name.Equals("Office Admin"))
                {
                    roleOfficeAdmin = r.Id;
                }
            }
        }
        
        //private bool CheckUserCredentials(string username, string password)
        //{
        //    using (var context = new DataContext())
        //    {
        //        return context.Users.Any(u => u.Username == username && u.Password == password);
        //    }
        //}

        //private void buttonLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    string username = usernameTextBox.Text;
        //    string password = passwordTextBox.Password;

        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //    {
        //        MessageBox.Show("Please enter your username and password");
        //        return;
        //    }

        //    bool isUserValid = CheckUserCredentials(username, password);

        //    if (isUserValid)
        //    {
        //        MessageBox.Show("Login successful");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid username or password");
        //    }
        //}
    }
}
