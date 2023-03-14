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
    /// Interaction logic for MechanicManageJobs.xaml
    /// </summary>
    public partial class MechanicManageJobs : Window
    {
        //IRepositories for all elements of the job
        IRepository<Customer> customerContext;
        IRepository<Job> jobContext;
        IRepository<User> userContext;
        IRepository<AssignedTo> assignedToContext;
        IRepository<Completed> completedContext;

        //get the logged in user
        User loggedInUser;

        //create a list for customers
        List<Customer> customersList;
        Customer selectedCustomer;
        int customerListsSize = 0;
        int customerPosition = 0;

        //create a list for jobs
        List<Job> jobsList;
        Job selectedJob;
        int jobListSize = 0;
        int jobPosition = 0;

        //create a list for users
        List<User> usersList;
        User selectedUser;
        int userListSize = 0;
        int userPosition = 0;

        //create a list for assigned to status
        List<AssignedTo> assignedTosList;
        AssignedTo selectedAssignedTo;
        int assignedToListSize = 0;
        int assignedToPosition = 0;

        //create a list for completed statuss
        List<Completed> completedsList;
        Completed seletedCompleted;
        int completedListSize = 0;
        int completedPosition = 0;
        public MechanicManageJobs(User u)
        {
            loggedInUser = u;

            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            this.jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
            this.assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();
            this.completedContext = ContainerHelper.Container.Resolve<IRepository<Completed>>();

            InitializeComponent();
            RefreshData();
        }

        //refresh data from the db
        private void RefreshData()
        {
            List<Customer> customerList = customerContext.Collection().ToList();
            cmbCustomer.ItemsSource = customerList;
            cmbCustomer.DisplayMemberPath = "Name";
            cmbCustomer.SelectedValuePath = "Id";

            customersList = customerContext.Collection().ToList();
            customerListsSize = customersList.Count();

            selectedCustomer = customersList.FirstOrDefault();
            customerPosition = customersList.IndexOf(selectedCustomer);


            List<Job> jobList = jobContext.Collection().ToList();
            jobsList = jobContext.Collection().ToList();
            jobListSize = jobsList.Count();

            selectedJob = jobsList.FirstOrDefault();
            jobPosition = jobsList.IndexOf(selectedJob);

            List<AssignedTo> assignedToList = assignedToContext.Collection().ToList();
            cmbAssignedTo.ItemsSource = assignedToList;
            cmbAssignedTo.DisplayMemberPath = "Name";
            cmbAssignedTo.SelectedValuePath = "Id";

            assignedTosList = assignedToContext.Collection().ToList();
            assignedToListSize = assignedTosList.Count();

            selectedAssignedTo = assignedTosList.FirstOrDefault();
            assignedToPosition = assignedTosList.IndexOf(selectedAssignedTo);

            List<Completed> completedList = completedContext.Collection().ToList();
            cmbCompleted.ItemsSource = completedList;
            cmbCompleted.DisplayMemberPath = "Name";
            cmbCompleted.SelectedValuePath = "Id";

            completedsList = completedContext.Collection().ToList();
            completedListSize = completedsList.Count();

            seletedCompleted = completedsList.FirstOrDefault();
            completedPosition = completedsList.IndexOf(seletedCompleted);

            //set values of fields 
            cmbCustomer.SelectedValue = selectedJob.CustomerName;
            txtDescription.Text = selectedJob.Description;
            txtPrice.Text = selectedJob.Price.ToString();
            cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
            cmbCompleted.SelectedValue = selectedJob.Completed;
            txtNotes.Text = selectedJob.Notes;
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            selectedCustomer = customersList.FirstOrDefault();
            //selectedUser = usersList.FirstOrDefault();
            selectedJob = jobsList.FirstOrDefault();
            //selectedAssignedTo = assignedTosList.FirstOrDefault();
            //selectedCompleted = completedsList.FirstOrDefault();

            customerPosition = customersList.IndexOf(selectedCustomer);
            userPosition = usersList.IndexOf(selectedUser);
            jobPosition = jobsList.IndexOf(selectedJob);

            cmbCustomer.SelectedValue = selectedJob.CustomerName;
            txtDescription.Text = selectedJob.Description;
            txtPrice.Text = selectedJob.Price.ToString();
            cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
            cmbCompleted.SelectedValue = selectedJob.Completed;
            txtNotes.Text = selectedJob.Notes;
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (jobPosition != 0)
            {
                selectedJob = jobsList[jobPosition - 1];
                jobPosition = jobsList.IndexOf(selectedJob);

                cmbCustomer.SelectedValue = selectedJob.CustomerName;
                txtDescription.Text = selectedJob.Description;
                txtPrice.Text = selectedJob.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
                cmbCompleted.SelectedValue = selectedJob.Completed;
                txtNotes.Text = selectedJob.Notes;
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (jobPosition != jobListSize - 1)
            {
                jobPosition = jobListSize - 1;
                selectedJob = jobsList[jobPosition];

                cmbCustomer.SelectedValue = selectedJob.CustomerName;
                txtDescription.Text = selectedJob.Description;
                txtPrice.Text = selectedJob.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
                cmbCompleted.SelectedValue = selectedJob.Completed;
                txtNotes.Text = selectedJob.Notes;
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (jobPosition != jobListSize - 1)
            {
                jobPosition++;
                selectedJob = jobsList[jobPosition];

                cmbCustomer.SelectedValue = selectedJob.CustomerName;
                txtDescription.Text = selectedJob.Description;
                txtPrice.Text = selectedJob.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
                cmbCompleted.SelectedValue = selectedJob.Completed;
                txtNotes.Text = selectedJob.Notes;
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            txtNotes.Text = selectedJob.Notes;

            jobContext.Update(selectedJob);
            await jobContext.Commit();
            MessageBox.Show("Record saved successfully!");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MechanicMenu hmm = new MechanicMenu(loggedInUser);
            hmm.Show();
        }

        private void ViewTasks(object sender, RoutedEventArgs e)
        {
            string jobID = selectedJob.Id;
            this.Hide();
            MechanicManageTasks hmmt = new MechanicManageTasks(loggedInUser, jobID);
            hmmt.Show();
        }
    }
}
