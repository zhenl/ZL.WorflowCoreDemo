
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.ControlStructures
{
    public class WhileWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "WhileWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {
            builder
                .StartWith<HelloWithName>()
                    .Input(step => step.Name, data => data.MyName)
                .While(data => data.MyName.Length < 3)
                    .Do(x => x
                        .StartWith(context=> { Console.WriteLine("输入小于3个字符"); ExecutionResult.Next(); })
                        .Activity("activity-1", (data) => data.MyName)
                        .Output(data => data.MyName, step => step.Result))
                .Then<GoodbyeWithName>()
                   .Input(step => step.Name, data => data.MyName);
        }
    }
}
