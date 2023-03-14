using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Models
{
    public class CompletedJob
    {
        public string CustomerName { get; set; }
        public string JobDescription { get; set; }
        public decimal Price { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }

        public CompletedJob(string customerName, string jobDescription, decimal price, string assignedTo, string status)
        {
            this.CustomerName = customerName;
            this.JobDescription = jobDescription;
            this.Price = price;
            this.AssignedTo = assignedTo;
            this.Status = status;
        }
    }
}
