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
        //    IRepository<Customer> customerContext;
        //    IRepository<Job> jobContext;
        //    IRepository<Task> taskContext;
        //    IRepository<Invoice> invoiceContext;
        //    IRepository<User> userContext;
        //    IRepository<Role> roleContext;
        //IRepository<AssignedTo> assignedToContext;

        //public SeedData()
        //{
            //    customerContext = ContainerHelper.Container.Resolve<IRepository<Customer>>();
            //    jobContext = ContainerHelper.Container.Resolve<IRepository<Job>>();
            //    taskContext = ContainerHelper.Container.Resolve<IRepository<Task>>();
            //    invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
            //    userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            //    roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            //assignedToContext = ContainerHelper.Container.Resolve<IRepository<AssignedTo>>();
        

    //    Console.WriteLine("***************************************************************");
    //    Console.WriteLine("WARNING: Remember you should only run Seed method once");
    //    Console.WriteLine("***************************************************************");
        }

        //public async void AddData()
        //{
            //    Customer customerOne = new Customer("Test Customer", "123 Street, Newcastle upon Tyne, NE1 7SA", "01234567896", "customer@email.com");
            //    customerContext.Insert(customerOne);
            //    Customer customerTwo = new Customer("Another Customer", "20 Street, Newcastle upon Tyne, NE1 7SA", "02345685264", "cus@email.com");
            //    customerContext.Insert(customerTwo);
            //    await customerContext.Commit();
            //    Console.WriteLine("Customer data successfully seeded.");

            //AssignedTo assignedOne = new AssignedTo("sysadmin");
            //assignedToContext.Insert(assignedOne);

            //AssignedTo assignedTwo = new AssignedTo("headmechanic");
            //assignedToContext.Insert(assignedTwo);

            //AssignedTo assignedThree = new AssignedTo("officeadmin");
            //assignedToContext.Insert(assignedThree);

            //AssignedTo assignedFour = new AssignedTo("mechanic");
            //assignedToContext.Insert(assignedFour);

            //await assignedToContext.Commit();
            //Console.WriteLine("Assigned To data successfully seeded.");

            //    Role roleOne = new Role("System Admin");
            //    roleContext.Insert(roleOne);
            //    Role roleTwo = new Role("Head Mechanic");
            //    roleContext.Insert(roleTwo);
            //    Role roleThree = new Role("Mechanic");
            //    roleContext.Insert(roleThree);
            //    Role roleFour = new Role("Office Admin");
            //    roleContext.Insert(roleFour);
            //    await roleContext.Commit();
            //    Console.WriteLine("Role data successfully seeded.");

            //    User userOne = new User("sysadmin", "Password12", roleOne.Id);
            //    userContext.Insert(userOne);
            //    User userTwo = new User("headmechanic", "Password12", roleTwo.Id);
            //    userContext.Insert(userTwo);
            //    User userThree = new User("mechanic", "Password12", roleThree.Id);
            //    userContext.Insert(userThree);
            //    User userFour = new User("officeadmin", "Password12", roleFour.Id);
            //    userContext.Insert(userFour);
            //    await userContext.Commit();
            //    Console.WriteLine("User data successfully seeded.");
        //}
    //}
}
