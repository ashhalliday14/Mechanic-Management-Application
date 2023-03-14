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
using System.Data.SqlClient;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for OfficeAdminManageJobs.xaml
    /// </summary>
    public partial class OfficeAdminManageJobs : Window
    {
        //IRepositories for all elements of the job
        IRepository<Customer> customerContext;
        IRepository<Job> jobContext;
        IRepository<User> userContext;
        IRepository<AssignedTo> assignedToContext;
        IRepository<Completed> completedContext;

        //get the logged in user
        User loggedInUser;

        AuditLog audit = new AuditLog();

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

        public OfficeAdminManageJobs(User u)
        {
            //set the logged in user
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
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            audit.LogAction("clicked to view first job", loggedInUser.ToString());
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
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (jobPosition != 0)
            {
                audit.LogAction("clicked to view previous job", loggedInUser.ToString());
                selectedJob = jobsList[jobPosition - 1];
                jobPosition = jobsList.IndexOf(selectedJob);

                cmbCustomer.SelectedValue = selectedJob.CustomerName;
                txtDescription.Text = selectedJob.Description;
                txtPrice.Text = selectedJob.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
                cmbCompleted.SelectedValue = selectedJob.Completed;
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (jobPosition != jobListSize - 1)
            {
                audit.LogAction("clicked to view next job", loggedInUser.ToString());
                jobPosition = jobListSize - 1;
                selectedJob = jobsList[jobPosition];

                cmbCustomer.SelectedValue = selectedJob.CustomerName;
                txtDescription.Text = selectedJob.Description;
                txtPrice.Text = selectedJob.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
                cmbCompleted.SelectedValue = selectedJob.Completed;
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (jobPosition != jobListSize - 1)
            {
                audit.LogAction("clicked to view last job", loggedInUser.ToString());
                jobPosition++;
                selectedJob = jobsList[jobPosition];

                cmbCustomer.SelectedValue = selectedJob.CustomerName;
                txtDescription.Text = selectedJob.Description;
                txtPrice.Text = selectedJob.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedJob.AssignedTo;
                cmbCompleted.SelectedValue = selectedJob.Completed;
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            const int MaxJobsPerUser = 3;
            string userID = cmbAssignedTo.SelectedValue.ToString();

            // Connect to the database and create a command to count the number of jobs assigned to the user
            using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=AdvancedProgramming;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand countCommand = new SqlCommand(
                    "SELECT COUNT(*) FROM dbo.Jobs WHERE AssignedTo = @UserID AND Completed = 1",
                    connection);
                countCommand.Parameters.AddWithValue("@UserID", userID);

                // Execute the command and check the result
                int jobCount = (int)countCommand.ExecuteScalar();
                if (jobCount >= MaxJobsPerUser)
                {
                    //throw new Exception("User has too many jobs assigned.");
                    MessageBox.Show("User has too many jobs assigned. Please select another user");
                    audit.LogAction("attempted to update job for user with too many jobs assigned", loggedInUser.ToString());
                }
                else
                {
                    selectedJob.CustomerName = cmbCustomer.SelectedValue.ToString();
                    selectedJob.Description = txtDescription.Text;
                    selectedJob.Price = Convert.ToDecimal(txtPrice.Text);
                    selectedJob.AssignedTo = cmbAssignedTo.SelectedValue.ToString();
                    selectedJob.Completed = cmbCompleted.SelectedValue.ToString();

                    jobContext.Update(selectedJob);
                    await jobContext.Commit();
                    MessageBox.Show("Record saved successfully!");
                    audit.LogAction("updated a job", loggedInUser.ToString());
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OfficeAdminMenu oam = new OfficeAdminMenu(loggedInUser);
            audit.LogAction("entered office admin menu", loggedInUser.ToString());
            oam.Show();
        }

        private void CreateJob(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OfficeAdminCreateJobs oicj = new OfficeAdminCreateJobs(loggedInUser);
            audit.LogAction("entered office admin create jobs page", loggedInUser.ToString());
            oicj.Show();
        }

        private void ViewTasks(object sender, RoutedEventArgs e)
        {
            string jobID = selectedJob.Id;
            this.Hide();
            OfficeAdminManageTasks oimt = new OfficeAdminManageTasks(loggedInUser, jobID);
            audit.LogAction("entered office admin manage tasks page", loggedInUser.ToString());
            oimt.Show();
        }

        private void ManageInvoice(object sender, RoutedEventArgs e)
        {
            string jobID = selectedJob.Id;
            this.Hide();
            OfficeAdminManageInvoices oimi = new OfficeAdminManageInvoices(loggedInUser, jobID);
            audit.LogAction("entered office admin manage invoices page", loggedInUser.ToString());
            oimi.Show();
        }
    }
}
