using TaskApplicationApi.Domain.ObjectValues.TaskEntity;

namespace TaskApplicationApi.Domain.Entities
{
    public class TaskEntity
    {
        protected TaskEntity()
        {
            
        }
        public Guid Id { get; init; }
        public TaskDescription Description { get; init; }
        public StatusTask Status { get; init; }
        public TaskCreationDate CreationDate { get; init; }
        public TaskDueDate DueDate { get; init; }
        public TaskPriority Priority { get; init; }
        public string UserId { get; init; }
        public virtual ICollection<TaskTags> Tags { get; set; }=new HashSet<TaskTags>();
        public TaskEntity(TaskDescription description,
            StatusTask status,
            string userId,
            TaskCreationDate creationDate,
            TaskDueDate dueDate,
            TaskPriority priority
            )
        {            
            Id = Guid.NewGuid();
            Description=description;
            Status=status;
            UserId=userId;
            CreationDate=creationDate;
            DueDate=dueDate;
            Priority=priority;
        }



    }
}
