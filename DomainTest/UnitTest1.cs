using FluentAssertions;
using TaskApplicationApi.Domain.Entities;
using TaskApplicationApi.Domain.ObjectValues.TaskEntity;
using TaskApplicationApi.Domain.Utils.Enums;

namespace DomainTest
{
    public class UnitTest1
    {
        private string Description;
        private StatusTask Status;
        private string Owner ;
        private DateTime CreationDate ;
        private DateTime DueDate ;
        private int Priority ;
        private string Tags ;
        public UnitTest1()
        {
            Description = "Description";
            Status= StatusTask.CreateFromEnum(TaskStatusEnum.Fulfilled);
            Owner="Owner";
            CreationDate = DateTime.Now.AddDays(1);
            DueDate = DateTime.Now.AddDays(3);
            Priority = 1;
            Tags="sas";
        }

        [Fact]
        public void TaskEntity_Should_Have_RightsProperties()
        {

            var newTask = new TaskEntity(Description,Status,Owner,CreationDate,DueDate,Priority,Tags);
            newTask.Status.Value.Should().Be("Fulfilled");
        }
    }
}