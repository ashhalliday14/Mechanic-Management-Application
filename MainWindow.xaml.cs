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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private bool CheckUserCredentials(string username, string password)
        {
            using (var context = new DataContext())
            {
                return context.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your username and password");
                return;
            }

            bool isUserValid = CheckUserCredentials(username, password);

            if(isUserValid)
            {
                MessageBox.Show("Login successful");
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
