using System;
using Xunit;
using WorkflowCore.Testing;
using ZL.WorflowCoreDemo.Basic;
using WorkflowCore.Models;

namespace ZL.WorkflowCoreDemo.Test
{
    public class DemoUnitTest:WorkflowTest<HelloWorldWorkflow,dynamic>
    {
        public DemoUnitTest()
        {
            Setup();
        }

        [Fact]
        public void Test1()
        {
            dynamic data = new { };
            var workflowId = StartWorkflow(data);
            WaitForWorkflowToComplete(workflowId, TimeSpan.FromSeconds(30));

            GetStatus(workflowId).Should().Be(WorkflowStatus.Complete);
            //UnhandledStepErrors.Count.Should().Be(0);
           
        }
    }
}
