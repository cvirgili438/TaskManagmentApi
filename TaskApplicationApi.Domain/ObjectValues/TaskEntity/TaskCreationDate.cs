using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class TaskCreationDate
    {
        protected TaskCreationDate()
        {

        }
        internal TaskCreationDate(DateTime date)
        {
            this.Value = date;
        }

        public DateTime Value { get; protected set; }
        public static TaskCreationDate Create(DateTime date) {
            var currentDate = DateTime.Now;
            if (date < currentDate)
            {
                throw new Exception("Current date is bigger than the prop date");
            }
            else return new TaskCreationDate(date);
        }
    }
}
