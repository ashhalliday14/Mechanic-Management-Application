using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Models
{
    //class for invoices that are created as jobs are completed
    public class Invoice : BaseEntity
    {
        public string JobID { get; set; }
        public string CustomerName { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentSchedule { get; set; }
        public DateTime Date { get; set; }

        //public Invoice(string JobID, string CustomerName, decimal AmountOwed, decimal AmountPaid, string PaymentSchedule, DateTime Date)
        //{
            //this.JobID = JobID;
            //this.CustomerName = CustomerName;
            //this.AmountOwed = AmountOwed;
            //this.AmountPaid = AmountPaid;
            //this.PaymentSchedule = PaymentSchedule;
            //this.Date = Date;
        //}
    }
}
