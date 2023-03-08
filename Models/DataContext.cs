using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedProgramming.Models;
using System.Data.Entity;
using DatabaseExample.Models;

namespace AdvancedProgramming.Models
{
    //this class handles communication with the database
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Completed> Completeds { get; set; }
        public DbSet<AssignedTo> AssignedTos { get; set; }
    }
}
