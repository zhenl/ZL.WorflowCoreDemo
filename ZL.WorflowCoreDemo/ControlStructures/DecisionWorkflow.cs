using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.ControlStructures
{
    public class DecisionWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "DecisionWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {
            var branch1 = builder.CreateBranch()
                .StartWith(context => { Console.WriteLine("输入小于3个字符"); ExecutionResult.Next(); });
            var branch2 = builder.CreateBranch()
                .StartWith(context => { Console.WriteLine("输入大于等于3个字符"); ExecutionResult.Next(); });

            builder
                .StartWith(context => ExecutionResult.Next())
                .Activity("activity-1", (data) => data.MyName)
                        .Output(data => data.MyName, step => step.Result)
                .Decide(data => data.MyName.Length)
                     .Branch((data, outcome) => data.MyName.Length<3, branch1)
                     .Branch((data, outcome) => data.MyName.Length >= 3, branch2)
                .Then<GoodbyeWithName>()
                   .Input(step => step.Name, data => data.MyName);
        }
    }
}
