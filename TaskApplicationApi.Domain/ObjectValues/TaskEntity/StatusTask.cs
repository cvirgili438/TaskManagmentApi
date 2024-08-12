using TaskApplicationApi.Domain.Utils.Enums;

namespace TaskApplicationApi.Domain.ObjectValues.TaskEntity
{
    public class StatusTask
    {
        public string Value { get; protected set; }
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

    }
}
