using AdvancedProgramming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseExample.Models
{
    //customer class which extends the base entity class
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //public Customer(string Name, string Address, string PhoneNumber, string Email)
        //{
            //this.Name = Name;
            //this.Address = Address;
            //this.PhoneNumber = PhoneNumber;
            //this.Email = Email;
        //}
    }
}
