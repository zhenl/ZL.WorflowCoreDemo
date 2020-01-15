using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.ControlStructures
{
    public class IfWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "IfWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {
            builder
                .StartWith(context=> ExecutionResult.Next())
                .Activity("activity-1", (data) => data.MyName)
                        .Output(data => data.MyName, step => step.Result)    
                .If(data => data.MyName.Length < 3)
                    .Do(then=>then
                        .StartWith(context => { Console.WriteLine("输入小于3个字符"); ExecutionResult.Next(); }))
                .If(data => data.MyName.Length >= 3)
                    .Do(then => then
                        .StartWith(context => { Console.WriteLine("输入大于等于3个字符"); ExecutionResult.Next(); }))
                .Then<GoodbyeWithName>()
                   .Input(step => step.Name, data => data.MyName);
        }
    }
}
