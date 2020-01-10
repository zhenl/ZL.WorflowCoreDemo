using System;
using System.Collections.Generic;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.InputDataToStep
{
    public class HelloWithNameWorkflowDynamic : IWorkflow<Dictionary<string,string>>
    {
        public string Id => "HelloWithNameWorkflowDynamic";
        public int Version => 1;

        public void Build(IWorkflowBuilder<Dictionary<string, string>> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())
                .WaitFor("MyEvent", (data, context) => context.Workflow.Id, data => DateTime.Now)
                    .Output((step,data)=>data.Add("Name",(string)step.EventData))
                .Then<HelloWithName>()
                    .Input(step => step.Name, data => data["Name"])
                .Then<GoodbyeWithName>()
                    .Input(step => step.Name, data => data["Name"])
                    .Then((context)=> { Console.WriteLine("结束"); });
        }
    }
}
