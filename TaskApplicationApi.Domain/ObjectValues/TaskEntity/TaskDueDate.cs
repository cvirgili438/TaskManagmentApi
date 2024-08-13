using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class TaskDueDate
    {
        public DateTime Value { get; }
        protected TaskDueDate() { }
        internal TaskDueDate(DateTime dueDate) 
        {
            this.Value=dueDate;
        }
        public static TaskDueDate CreateFromDateTime(DateTime dueDate) 
        {
            if (dueDate<DateTime.Now) 
            {
                throw new Exception("DueDate is lower than current time");
            }
            else return new TaskDueDate(dueDate);

        }
        public override bool Equals(object obj)
        {
            if (obj is TaskDueDate other)
                return Value == other.Value;

            return false;
        }

        // Sobrescribe GetHashCode para que coincida con la implementación de Equals
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

    }
}
