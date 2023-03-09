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
    /// Interaction logic for ManageTasks.xaml
    /// </summary>
    public partial class ManageTasks : Window
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
        public ManageTasks(User u, string jobID)
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
            cmbAssignedTo.SelectedValue = selectedAssignedTo.Id;
            cmbCompleted.SelectedValue = seletedCompleted.Id;
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageJobs ct = new ManageJobs(loggedInUser);
            ct.Show();
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
            cmbAssignedTo.SelectedValue = selectedAssignedTo.Id;
            cmbCompleted.SelectedValue = seletedCompleted.Id;
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
                cmbAssignedTo.SelectedValue = selectedAssignedTo.Id;
                cmbCompleted.SelectedValue = seletedCompleted.Id;
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
                cmbAssignedTo.SelectedValue = selectedAssignedTo.Id;
                cmbCompleted.SelectedValue = seletedCompleted.Id;
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
                cmbAssignedTo.SelectedValue = selectedAssignedTo.Id;
                cmbCompleted.SelectedValue = seletedCompleted.Id;
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            selectedTask.JobID = txtJobID.Text;
            selectedTask.TaskName = txtTaskName.Text;
            selectedTask.Description = txtDescription.Text;
            selectedTask.Price = Convert.ToDecimal(txtPrice.Text);
            selectedTask.AssignedTo = cmbAssignedTo.SelectedValue.ToString();
            selectedTask.Completed = cmbCompleted.SelectedValue.ToString();

            taskContext.Update(selectedTask);
            await taskContext.Commit();
            MessageBox.Show("Record saved successfully!");
        }

        private async void DeleteTask(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string jobID = txtJobID.Text;
                taskContext.Delete(selectedTask.Id);
                await taskContext.Commit();
                MessageBox.Show("Task has been successfully deleted!");
                RefreshData(jobID);
            }
        }
    }
}
