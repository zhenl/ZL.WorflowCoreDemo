
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.Paralle
{
    public class ParalleWorkflow : IWorkflow
    {
        public string Id => "ParalleWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
            .StartWith(context => { Console.WriteLine("开始"); ExecutionResult.Next(); })
            .ForEach(data => new List<string>() { "张三", "李四", "王五", "赵六" })
                .Do(x => x
                    .StartWith<HelloWithName>()
                        .Input(step => step.Name, (data, context) => context.Item as string)
                    .Then<GoodbyeWithName>()
                        .Input(step => step.Name, (data, context) => context.Item as string)
                    )
            .Then(context => { Console.WriteLine("结束"); ExecutionResult.Next(); });
        }
    }
}
