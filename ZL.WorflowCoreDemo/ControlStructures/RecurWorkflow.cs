using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.ControlStructures
{
    public class RecurWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "RecurWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {
            builder
                .StartWith(context => Console.WriteLine("开始"))
                    .Recur(data => TimeSpan.FromSeconds(5),data=>data.MyName.Length>5).Do(recur => recur
                    .StartWith<HelloWithName>()
                    .Input(step => step.Name, data => data.MyName))
                .Then(context => Console.WriteLine("前台工作"))
                .Activity("activity-1", (data) => data.MyName)
                        .Output(data => data.MyName, step => step.Result);
        }
    }
}
