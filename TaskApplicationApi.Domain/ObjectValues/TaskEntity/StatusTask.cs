using TaskApplicationApi.Domain.Utils.Enums;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class StatusTask
    {
        public string Value { get; }
        protected StatusTask()
        {

        }
        internal StatusTask(TaskStatusEnum value) 
        {
            this.Value = value.ToString();
        }
        public static StatusTask CreateFromEnum(TaskStatusEnum value) 
        {
          return new StatusTask(value);
        }
        public override bool Equals(object obj)
        {
            if (obj is StatusTask other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

    }
}
