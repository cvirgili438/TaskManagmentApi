using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Domain.Utils.Enums;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class TaskPriority
    {
        public string Value { get; }
        protected TaskPriority()
        {
            
        }
        public TaskPriority(TaskPriorityEnum value)
        {
            this.Value=value.ToString();
        }
        public static TaskPriority CreateFromEnum(TaskPriorityEnum value) 
        {
            return new TaskPriority(value);
        }
        public override bool Equals(object obj)
        {
            if (obj is TaskPriority other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
