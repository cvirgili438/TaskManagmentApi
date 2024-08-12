
using TaskApplicationApi.Domain.ObjectValues.TaskEntity;

namespace TaskApplicationApi.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public StatusTask Status { get; init; }
        public string Owner { get; init; }
        public DateTime CreationDate { get; init; }
        public DateTime DueDate { get; init; }
        public int Priority { get; init; }
        public string Tags { get; init; }
        public TaskEntity(string description,
            StatusTask status,
            string owner,
            DateTime creationDate,
            DateTime dueDate,
            int priority,
            string tags)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException($"'{nameof(description)}' no puede ser nulo ni estar vacío.", nameof(description));
            }

            if (status is null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            if (string.IsNullOrEmpty(owner))
            {
                throw new ArgumentException($"'{nameof(owner)}' no puede ser nulo ni estar vacío.", nameof(owner));
            }

            if (string.IsNullOrEmpty(tags))
            {
                throw new ArgumentException($"'{nameof(tags)}' no puede ser nulo ni estar vacío.", nameof(tags));
            }
            Id = Guid.NewGuid();
            Description=description;
            Status=status;
            Owner=owner;
            CreationDate=creationDate;
            DueDate=dueDate;
            Priority=priority;
            Tags=tags;
        }



    }
}
