using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Domain.Entities
{
    public class TaskTags
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public virtual ICollection<TaskEntity> Tasks { get; set; } = new HashSet<TaskEntity>();
        public TaskTags(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" no puede ser NULL ni un espacio en blanco.", nameof(name));
            }
            if (name.Length>25) 
            {
                throw new ArgumentException($"{nameof(name)} shouldn't be larger than 25",nameof(name));
            }
            Name=name;
        }
    }
}
