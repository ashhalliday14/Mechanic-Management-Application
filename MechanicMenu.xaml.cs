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
using AdvancedProgramming.Contracts;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using Unity;
using DatabaseExample.Models;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for MechanicMenu.xaml
    /// </summary>
    public partial class MechanicMenu : Window
    {
        User loggedInUser;
        List<string> messages;

        AuditLog audit = new AuditLog();

        //IRepositories for all elements of the job
        IRepository<Customer> customerContext;
        IRepository<Job> jobContext;
        IRepository<User> userContext;
        IRepository<AssignedTo> assignedToContext;
        IRepository<Completed> completedContext;
        IRepository<Task> taskContext;

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

        //create a list for tasks
        List<Task> tasksList;
        Task selectedTask;
        int taskListSize = 0;
        int taskPosition = 0;

        public MechanicMenu(User u)
        {
            loggedInUser = u;

            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            this.jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
            this.assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();
            this.completedContext = ContainerHelper.Container.Resolve<IRepository<Completed>>();
            this.taskContext = ContainerHelper.Container.Resolve<IRepository<Task>>();

            InitializeComponent();
            LoadMessages();
        }

        private void ManageJobs(object sender, RoutedEventArgs e)
        {
            Hide();
            MechanicManageJobs mmj = new MechanicManageJobs(loggedInUser);
            audit.LogAction("entered mechanic manage jobs page", loggedInUser.ToString());
            mmj.Show();
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
            audit.LogAction("refreshed message on dashboatd", loggedInUser.ToString());
        }

        private void RefreshTasks(object sender, RoutedEventArgs e)
        {
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


            List<Task> taskList = taskContext.Collection().ToList();

            tasksList = taskContext.Collection().ToList();
            taskListSize = tasksList.Count();

            selectedTask = tasksList.FirstOrDefault();
            taskPosition = tasksList.IndexOf(selectedTask);

            //set values of fields 
            txtTaskName.Text = selectedTask.TaskName;
            txtDescription.Text = selectedTask.Description;
            cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
            cmbCompleted.SelectedValue = selectedTask.Completed;
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (taskPosition != 0)
            {
                audit.LogAction("clicked to view previous task", loggedInUser.ToString());
                selectedTask = tasksList[taskPosition - 1];
                taskPosition = tasksList.IndexOf(selectedTask);

                txtTaskName.Text = selectedTask.TaskName;
                txtDescription.Text = selectedTask.Description;
                cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
                cmbCompleted.SelectedValue = selectedTask.Completed;
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (taskPosition != taskListSize - 1)
            {
                audit.LogAction("clicked to view next task", loggedInUser.ToString());
                taskPosition = taskListSize - 1;
                selectedTask = tasksList[taskPosition];

                txtTaskName.Text = selectedTask.TaskName;
                txtDescription.Text = selectedTask.Description;
                cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
                cmbCompleted.SelectedValue = selectedTask.Completed;
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            audit.LogAction("logged out", loggedInUser.ToString());
            System.Windows.Application.Current.Shutdown();
        }
    }
}
