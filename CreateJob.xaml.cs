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
    /// Interaction logic for CreateJob.xaml
    /// </summary>
    public partial class CreateJob : Window
    {
        User loggedInUser;

        IRepository<Customer> customerContext;
        IRepository<Job> jobContext;
        IRepository<User> userContext;
        IRepository<Completed> completedContext;
        IRepository<AssignedTo> assignedToContext;
        public CreateJob(User u)
        {
            loggedInUser = u;

            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            this.jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
            this.completedContext = ContainerHelper.Container.Resolve<IRepository<Completed>>();
            this.assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();

            InitializeComponent();

            List<Customer> customerList = customerContext.Collection().ToList();
            cmbCustomer.ItemsSource = customerList;
            cmbCustomer.DisplayMemberPath = "Name";
            cmbCustomer.SelectedValuePath = "Id";
            cmbCustomer.SelectedItem = null;

            List<User> userList = userContext.Collection().ToList();
            cmbAssignedTo.ItemsSource = userList;
            cmbAssignedTo.DisplayMemberPath = "Name";
            cmbAssignedTo.SelectedValuePath = "Id";
            cmbAssignedTo.SelectedItem = null;

            //List<Job> jobList = jobContext.Collection().ToList();
            //cmbCompleted.ItemsSource = jobList;
            //cmbCompleted.DisplayMemberPath = "Name";
            //cmbCompleted.SelectedValuePath = "Id";
            //cmbCompleted.SelectedItem = null;

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
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageJobs mj = new ManageJobs(loggedInUser);
            mj.Show();
        }

        private async void Create(object sender, RoutedEventArgs e)
        {
            if (cmbCustomer.SelectedItem == null || txtDescription.Equals("") || txtPrice.Equals("") || cmbAssignedTo.SelectedItem == null || cmbCompleted.SelectedItem == null)
            {
                MessageBox.Show("Please enter all required fields before creating a new job");
            }
            else
            {
                Job job = new Job();
                job.CustomerName = cmbCustomer.SelectedValue.ToString();
                job.Description = txtDescription.Text;
                job.Price = Convert.ToDecimal(txtPrice.Text);
                job.AssignedTo = cmbAssignedTo.SelectedValue.ToString();
                job.Completed = cmbCompleted.SelectedValue.ToString();
                
                jobContext.Insert(job);
                await jobContext.Commit();
                MessageBox.Show("Job has been successfully created");
                ManageJobs mj = new ManageJobs(loggedInUser);
                this.Hide();
                mj.Show();
            }
        }
    }
}
