using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        User loggedInUser;

        AuditLog audit = new AuditLog();

        IRepository<Customer> customerContext;
        IRepository<Job> jobContext;
        IRepository<Task> taskContext;
        IRepository<User> userContext;
        IRepository<Completed> completedContext;
        IRepository<AssignedTo> assignedToContext;
        public CreateTask(User u, string jobID)
        {
            loggedInUser = u;

            MessageBox.Show("Job ID is " + jobID);

            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            this.jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
            this.completedContext = ContainerHelper.Container.Resolve<IRepository<Completed>>();
            this.assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();
            this.taskContext = ContainerHelper.Container.Resolve<IRepository<Task>>();

            InitializeComponent();

            //List<Customer> customerList = customerContext.Collection().ToList();
            //cmbCustomer.ItemsSource = customerList;
            //cmbCustomer.DisplayMemberPath = "Name";
            //cmbCustomer.SelectedValuePath = "Id";
            //cmbCustomer.SelectedItem = null;

            List<User> userList = userContext.Collection().ToList();
            cmbAssignedTo.ItemsSource = userList;
            cmbAssignedTo.DisplayMemberPath = "Name";
            cmbAssignedTo.SelectedValuePath = "Id";
            cmbAssignedTo.SelectedItem = null;

            List<Job> jobList = jobContext.Collection().ToList();
            //cmbJob.ItemsSource = jobList;
            //cmbJob.DisplayMemberPath = "Name";
            //cmbJob.SelectedValuePath = "Id";
            //cmbJob.SelectedItem = null;

            List<Task> taskList = taskContext.Collection().ToList();
            cmbCompleted.ItemsSource = jobList;
            cmbCompleted.DisplayMemberPath = "Name";
            cmbCompleted.SelectedValuePath = "Id";
            cmbCompleted.SelectedItem = null;

            List<Completed> completedList = completedContext.Collection().ToList();
            cmbCompleted.ItemsSource = completedList;
            cmbCompleted.DisplayMemberPath = "Name";
            cmbCompleted.SelectedValuePath = "Id";
            cmbCompleted.SelectedItem = null;

            List<AssignedTo> assignedToList = assignedToContext.Collection().ToList();
            cmbAssignedTo.ItemsSource = assignedToList;
            cmbAssignedTo.DisplayMemberPath = "Name";
            cmbAssignedTo.SelectedValuePath = "Id";
            cmbAssignedTo.SelectedItem = null;

            txtJobID.Text = jobID;
        }

        private async void Create(object sender, RoutedEventArgs e)
        {
            if (txtJobID.Equals("") || txtTaskName.Equals("") || txtDescription.Equals("") || txtPrice.Equals("") 
                || cmbAssignedTo.SelectedItem == null || cmbCompleted.SelectedItem == null)
            {
                MessageBox.Show("Please enter all required fields before creating a new task");
                audit.LogAction("attempted to create task with missing details", loggedInUser.ToString());
            }
            else
            {
                const int MaxTasksPerUser = 10;
                string userID = cmbAssignedTo.SelectedValue.ToString();

                // Connect to the database and create a command to count the number of jobs assigned to the user
                using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=AdvancedProgramming;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand countCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.Tasks WHERE AssignedTo = @UserID AND Completed = 1",
                        connection);
                    countCommand.Parameters.AddWithValue("@UserID", userID);

                    // Execute the command and check the result
                    int jobCount = (int)countCommand.ExecuteScalar();
                    if (jobCount >= MaxTasksPerUser)
                    {
                        //throw new Exception("User has too many tasks assigned.");
                        audit.LogAction("attempted to create task for a user with too many tasks assigned", loggedInUser.ToString());
                        MessageBox.Show("User has too many tasks assigned. Please select another user");
                    }
                    else
                    {
                        Task task = new Task();
                        task.JobID = txtJobID.Text;
                        task.TaskName = txtTaskName.Text;
                        task.Description = txtDescription.Text;
                        task.Price = Convert.ToDecimal(txtPrice.Text);
                        task.AssignedTo = cmbAssignedTo.SelectedValue.ToString();
                        task.Completed = cmbCompleted.SelectedValue.ToString();

                        taskContext.Insert(task);
                        await taskContext.Commit();
                        audit.LogAction("created a new task", loggedInUser.ToString());
                        MessageBox.Show("Task has been successfully created");
                    }
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Hide();
            audit.LogAction("returned to manage jobs page", loggedInUser.ToString());
            ManageJobs mj = new ManageJobs(loggedInUser);
            mj.Show();
        }
    }
}
