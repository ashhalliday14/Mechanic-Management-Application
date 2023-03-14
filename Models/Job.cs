using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Models
{
    //class for jobs being performed on vehicles
    public class Job : BaseEntity
    {
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string AssignedTo { get; set; }
        public string Completed { get; set; }
        public string Notes { get; set; }

        //public Job(string CustomerName, string Description, decimal Price, string AssignedTo, string Completed, string Notes)
        //{
            //this.CustomerName = CustomerName;
            //this.Description = Description;
            //this.Price = Price;
            //this.AssignedTo = AssignedTo;
            //this.Completed = Completed;
            //this.Notes = Notes;
        //}
    }
}
