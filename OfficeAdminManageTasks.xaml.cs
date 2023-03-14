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

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for OfficeAdminManageTasks.xaml
    /// </summary>
    public partial class OfficeAdminManageTasks : Window
    {
        User loggedInUser;

        IRepository<AssignedTo> assignedToContext;
        IRepository<Completed> completedContext;
        IRepository<Task> taskContext;

        //create a list for assigned to status
        List<AssignedTo> assignedTosList;
        AssignedTo selectedAssignedTo;
        int assignedToListSize = 0;
        int assignedToPosition = 0;

        //create a list for completed status
        List<Completed> completedsList;
        Completed seletedCompleted;
        int completedListSize = 0;
        int completedPosition = 0;

        //create a list for task
        List<Task> tasksList;
        Task selectedTask;
        int taskListSize = 0;
        int taskPosition = 0;
        public OfficeAdminManageTasks(User u, string jobID)
        {
            loggedInUser = u;

            this.assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();
            this.completedContext = ContainerHelper.Container.Resolve<IRepository<Completed>>();
            this.taskContext = ContainerHelper.Container.Resolve<IRepository<Task>>();

            MessageBox.Show("Job ID is " + jobID);
            InitializeComponent();
            RefreshData(jobID);
        }

        private void RefreshData(string jobID)
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
            txtJobID.Text = jobID;
            txtTaskName.Text = selectedTask.TaskName;
            txtDescription.Text = selectedTask.Description;
            txtPrice.Text = selectedTask.Price.ToString();
            cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
            cmbCompleted.SelectedValue = selectedTask.Completed;
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OfficeAdminManageJobs oimj = new OfficeAdminManageJobs(loggedInUser);
            oimj.Show();
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            selectedTask = tasksList.FirstOrDefault();
            selectedAssignedTo = assignedTosList.FirstOrDefault();
            seletedCompleted = completedsList.FirstOrDefault();

            taskPosition = tasksList.IndexOf(selectedTask);
            assignedToPosition = assignedTosList.IndexOf(selectedAssignedTo);
            completedPosition = completedsList.IndexOf(seletedCompleted);

            txtJobID.Text = selectedTask.JobID;
            txtTaskName.Text = selectedTask.TaskName;
            txtDescription.Text = selectedTask.Description;
            txtPrice.Text = selectedTask.Price.ToString();
            cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
            cmbCompleted.SelectedValue = selectedTask.Completed;
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (taskPosition != 0)
            {
                selectedTask = tasksList[taskPosition - 1];
                taskPosition = tasksList.IndexOf(selectedTask);

                txtJobID.Text = selectedTask.JobID;
                txtTaskName.Text = selectedTask.TaskName;
                txtDescription.Text = selectedTask.Description;
                txtPrice.Text = selectedTask.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
                cmbCompleted.SelectedValue = selectedTask.Completed;
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (taskPosition != taskListSize - 1)
            {
                taskPosition = taskListSize - 1;
                selectedTask = tasksList[taskPosition];

                txtJobID.Text = selectedTask.JobID;
                txtTaskName.Text = selectedTask.TaskName;
                txtDescription.Text = selectedTask.Description;
                txtPrice.Text = selectedTask.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
                cmbCompleted.SelectedValue = selectedTask.Completed;
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (taskPosition != taskListSize - 1)
            {
                taskPosition++;
                selectedTask = tasksList[taskPosition];

                txtJobID.Text = selectedTask.JobID;
                txtTaskName.Text = selectedTask.TaskName;
                txtDescription.Text = selectedTask.Description;
                txtPrice.Text = selectedTask.Price.ToString();
                cmbAssignedTo.SelectedValue = selectedTask.AssignedTo;
                cmbCompleted.SelectedValue = selectedTask.Completed;
            }
        }

    }
}
