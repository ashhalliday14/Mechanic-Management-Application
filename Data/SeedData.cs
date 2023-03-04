using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using AdvancedProgramming.Contracts;
using AdvancedProgramming.Models;
using DatabaseExample.Models;
using Unity;

namespace AdvancedProgramming.Data
{
    public class SeedData
    {
        //IRepository<Customer> customerContext;
        //IRepository<Job> jobContext;
        //IRepository<Task> taskContext;
        //IRepository<Invoice> invoiceContext;
        //IRepository<User> userContext;

        //public SeedData()
        //{
            //customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            //jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
            //taskContext = ContainerHelper.Container.Resolve<IRepository<Task>>();
            //invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
            //userContext = ContainerHelper.Container.Resolve<IRepository<User>>();

            //Console.WriteLine("***************************************************************");
            //Console.WriteLine("WARNING: Remember you should only run Seed method once");
            //Console.WriteLine("***************************************************************");
        //}

        //public async void AddData()
        //{
            //Customer customerOne = new Customer("Test Customer", "123 Street, Newcastle upon Tyne, NE1 7SA", "01234567896", "customer@email.com");
            //customerContext.Insert(customerOne);
            //Customer customerTwo = new Customer("Another Customer", "20 Street, Newcastle upon Tyne, NE1 7SA", "02345685264", "cus@email.com");
            //customerContext.Insert(customerTwo);
            //await customerContext.Commit();
            //Console.WriteLine("Customer data successfully seeded.");

            //User userOne = new User("sysadmin", "Password12", "System Admin");
            //userContext.Insert(userOne);
            //User userTwo = new User("headmechanic", "Password12", "Head Mechanic");
            //userContext.Insert(userTwo);
            //User userThree = new User("mechanic", "Password12", "Mechanic");
            //userContext.Insert(userThree);
            //User userFour = new User("officeadmin", "Password12", "Office Admin");
            //userContext.Insert(userFour);
            //await userContext.Commit();
            //Console.WriteLine("User data successfully seeded.");
        //}
    }
}
