using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Domain.ObjectValues.TaskEntity;
using TaskApplicationApi.Domain.Utils.Enums;

namespace DomainTest.ObjectValues.Task
{
    public class TestStatus
    {
        private string _fulfilled = TaskStatusEnum.Fulfilled.ToString();
        private string _pending = TaskStatusEnum.Pending.ToString();
        private string _canceled = TaskStatusEnum.Canceled.ToString();


        [Fact]
        public void Should_Create_From_Enum() 
        {
            var should_Be_Fullfilled = StatusTask.CreateFromEnum(TaskStatusEnum.Fulfilled);
            var should_Be_Pending = StatusTask.CreateFromEnum(TaskStatusEnum.Pending);
            var should_Be_Canceled = StatusTask.CreateFromEnum(TaskStatusEnum.Canceled);
            should_Be_Fullfilled.Value.Should().Be(_fulfilled);
            should_Be_Pending.Value.Should().Be(_pending);
            should_Be_Canceled.Value.Should().Be(_canceled);    
        }
        [Fact]
        public void Two_Instances_Should_Be_Equals() 
        {
            var instance1_Pending = StatusTask.CreateFromEnum(TaskStatusEnum.Pending);
            var instance2_Pending = StatusTask.CreateFromEnum(TaskStatusEnum.Pending);
            instance1_Pending.Equals(instance2_Pending).Should().BeTrue();
            (instance1_Pending==instance2_Pending).Should().BeFalse();
        }
        [Fact]
        public void Two_Instances_Should_Have_Same_HashCode() 
        {
            var instance1_Pending = StatusTask.CreateFromEnum(TaskStatusEnum.Pending);
            var instance2_Pending = StatusTask.CreateFromEnum(TaskStatusEnum.Pending);
            instance1_Pending.GetHashCode().Should().Be(instance2_Pending.GetHashCode());
        }
    }
}
