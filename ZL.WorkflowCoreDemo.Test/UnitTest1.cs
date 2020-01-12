using System;
using Xunit;
using WorkflowCore.Testing;
using ZL.WorflowCoreDemo.Basic;
using WorkflowCore.Models;
using System.Threading;
using FluentAssertions;

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

            WorkflowStatus status = GetStatus(workflowId);
            status.Should().Be(WorkflowStatus.Complete);
            UnhandledStepErrors.Count.Should().Be(0);
           
        }

        protected new WorkflowStatus GetStatus(string workflowId)
        {
            var instance = PersistenceProvider.GetWorkflowInstance(workflowId).Result;
            return instance.Status;
        }

        protected new void WaitForWorkflowToComplete(string workflowId, TimeSpan timeOut)
        {
            var status = GetStatus(workflowId);
            var counter = 0;
            while ((status == WorkflowStatus.Runnable) && (counter < (timeOut.TotalMilliseconds / 100)))
            {
                Thread.Sleep(100);
                counter++;
                status = GetStatus(workflowId);
            }
        }
    }
}
