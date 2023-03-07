using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using AdvancedProgramming.Contracts;
using AdvancedProgramming.Data;
using AdvancedProgramming.Models;
using DatabaseExample.Models;
using Unity;
using Unity.Lifetime;

namespace AdvancedProgramming
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRepository<Customer>, SQLRepository<Customer>>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<Job>, SQLRepository<Job>>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<Task>, SQLRepository<Task>>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<User>, SQLRepository<User>>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<Role>, SQLRepository<Role>>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<Invoice>, SQLRepository<Invoice>>(
                new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
