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

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for SystemAdminMenu.xaml
    /// </summary>
    public partial class SystemAdminMenu : Window
    {
        User loggedInUser;
        List<string> messages;
        public SystemAdminMenu(User u)
        {
            loggedInUser = u;
            InitializeComponent();
            LoadMessages();
        }

        private void ManageSystemUsers(object sender, RoutedEventArgs e)
        {
            Hide();
            ManageSystemUsers msu = new ManageSystemUsers(loggedInUser);
            msu.Show();
        }

        private void ManageCustomers(object sender, RoutedEventArgs e)
        {
            Hide();
            ManageCustomers mc = new ManageCustomers(loggedInUser);
            mc.Show();
        }

        private void ManageJobs(object sender, RoutedEventArgs e)
        {
            Hide();
            ManageJobs mj = new ManageJobs(loggedInUser);
            mj.Show();
        }

        private void ManageTasks(object sender, RoutedEventArgs e)
        {
            Hide();
            ManageTasks mt = new ManageTasks(loggedInUser);
            mt.Show();
        }

        public void LoadMessages()
        {
            messages = new List<string>();
            messages.Add("If you let your head get too big, it’ll break your neck. Elvis Presley");
            messages.Add("Bad decisions make good stories.s – Ellis Vidler");
            messages.Add("If the world didn’t suck we’d all fly into space. – Unknown");
            messages.Add("A diamond is merely a lump of coal that did well under pressure. – Unknown");
            messages.Add("Trying is the first step toward failure. – Homer Simpson");
            messages.Add("Hard work never killed anybody, but why take a chance? — Edgar Bergen");
            messages.Add("Beat the 5 o'clock rush, leave work at noon. — Anonymous");
            messages.Add("I hate when I lose things at work, like pens, papers, sanity and dreams. – Anonymous");
            messages.Add("If at first you don't succeed, then skydiving definitely isn't for you. – Steven Wright");

            var random = new Random();
            int index = random.Next(messages.Count);
            lblMessage.Text = messages[index];
        }

        private void RefreshMessage(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            int index = random.Next(messages.Count);
            lblMessage.Text = messages[index];
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
