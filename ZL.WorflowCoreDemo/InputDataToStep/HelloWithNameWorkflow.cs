using System;

using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.InputDataToStep
{
    public class HelloWithNameWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "HelloWithNameWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())
                .WaitFor("MyEvent", (data, context) => context.Workflow.Id, data => DateTime.Now)
                    .Output(data => data.MyName, step => step.EventData)
                .Then<HelloWithName>()
                    .Input(step => step.Name, data => data.MyName)
                .Then<GoodbyeWithName>()
                    .Input(step => step.Name, data => data.MyName);
        }
    }
}
