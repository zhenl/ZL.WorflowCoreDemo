using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep;

namespace ZL.WorflowCoreDemo.Paralle
{
    public class ParallePathWorkflow : IWorkflow
    {
        public string Id => "ParallePathWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
            .StartWith(context => { Console.WriteLine("开始"); ExecutionResult.Next(); })
            .Parallel()
                .Do(then =>
                    then.StartWith(context=>{ Console.WriteLine("分支一开始"); ExecutionResult.Next(); })
                        .Then(context => { Console.WriteLine("分支一结束"); ExecutionResult.Next(); }))
                .Do(then =>
                    then.StartWith(context => { Console.WriteLine("分支二开始"); ExecutionResult.Next(); })
                        .Then(context => { Console.WriteLine("分支二结束"); ExecutionResult.Next(); }))
                .Do(then =>
                    then.StartWith(context => { Console.WriteLine("分支二开始"); ExecutionResult.Next(); })
                        .Then(context => { Console.WriteLine("分支二结束"); ExecutionResult.Next(); }))
            .Join()
            .Then(context => { Console.WriteLine("结束"); ExecutionResult.Next(); });
        }
    }
}
