using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class TaskCreationDate
    {
        public DateTime Value { get; }
        protected TaskCreationDate()
        {
            
        }
        internal TaskCreationDate(DateTime value) 
        {
            this.Value = value;
        }
        public static TaskCreationDate CreateFromDateTime(DateTime date) 
        {
            if (date < DateTime.Now) 
            {
                throw new Exception("DateTime is old");
            }
            else return new TaskCreationDate(date);
        }
        public override bool Equals(object obj)
        {
            if (obj is TaskCreationDate other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
