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
using System.IO;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for AuditTrail.xaml
    /// </summary>
    public partial class AuditTrail : Window
    {
        User loggedInUser;
        public AuditTrail(User u)
        {
            loggedInUser = u;
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SystemAdminMenu sam = new SystemAdminMenu(loggedInUser);
            sam.Show();
        }

        private void ViewAuditTrail(object sender, RoutedEventArgs e)
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"C:\Users\ashle\source\repos\AdvancedProgramming\bin\Debug\auditlog.txt");
                //Read the first line of text
                // Read the contents of the file into a string
                string contents = sr.ReadToEnd();

                // Close the StreamReader object to free up resources
                sr.Close();

                // Set the contents of the text block to the file contents
                txtAuditTrail.Text = contents;
            }
            catch (Exception r)
            {
                Console.WriteLine("Exception: " + r.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
