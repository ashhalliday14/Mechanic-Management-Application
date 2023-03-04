using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Models
{
    //this is the base class for all over classes in this application
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        //public BaseEntity()
        //{
            //this.Id = Guid.NewGuid().ToString();
            //this.CreatedAt = DateTime.Now;
        //}
    }
}