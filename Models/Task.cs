using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedProgramming.Models;

namespace AdvancedProgramming.Models
{
    //class for tasks being performed as part of jobs
    public class Task : BaseEntity
    {
        public string JobID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string AssignedTo { get; set; }
        public string Completed { get; set; }
        public string Notes { get; set; }

        //public Task(string JobID, string TaskName, string Description, decimal Price, string AssignedTo, string Completed, string Notes)
        //{
            //this.JobID = JobID;
            //this.TaskName = TaskName;
            //this.Description = Description;
            //this.Price = Price;
            //this.AssignedTo = AssignedTo;
            //this.Completed = Completed;
            //this.Notes = Notes;
       //}
    }
}

