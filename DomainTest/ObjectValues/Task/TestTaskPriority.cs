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
    public class TestTaskPriority
    {
        private string _low = TaskPriorityEnum.Low.ToString();
        private string _medium = TaskPriorityEnum.Medium.ToString();
        private string _high = TaskPriorityEnum.High.ToString();
        [Fact]
        public void ShouldBe_Created_From_Enum() 
        {
            var lowPriority = TaskPriority.CreateFromEnum(TaskPriorityEnum.Low);
            var mediumPriority = TaskPriority.CreateFromEnum(TaskPriorityEnum.Medium);
            var highPriority = TaskPriority.CreateFromEnum(TaskPriorityEnum.High);
            lowPriority.Value.Should().Be(_low);
            mediumPriority.Value.Should().Be(_medium);
            highPriority.Value.Should().Be(_high);
        }
        [Fact]
        public void Should_Two_Instances_Be_Equals() 
        {
            var lowPriority = TaskPriority.CreateFromEnum(TaskPriorityEnum.Low);
            var lowPriority2 = TaskPriority.CreateFromEnum(TaskPriorityEnum.Low);
            lowPriority.Equals(lowPriority2).Should().BeTrue();

        }
    }
}
