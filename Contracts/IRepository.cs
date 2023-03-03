using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedProgramming.Models;

namespace AdvancedProgramming.Contracts
{

    //interface that provides a common set of methods to access data in the DB for all CRUD actions
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        Task<bool> Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}
