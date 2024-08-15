using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class TaskDescription
    {
        public string Value { get; }
        protected TaskDescription()
        {
            
        }
        internal TaskDescription(string value) 
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"'{nameof(value)}' no puede ser nulo ni estar vacío.", nameof(value));
            }
            this.Value = value;
        }
        public static TaskDescription CreateFromString(string value) 
        {
            return new TaskDescription(value);
        }
        public override bool Equals(object obj)
        {
            if (obj is TaskDescription other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
