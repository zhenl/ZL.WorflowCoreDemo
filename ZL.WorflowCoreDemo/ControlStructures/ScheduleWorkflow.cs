using System;
using WorkflowCore.Interface;


namespace ZL.WorflowCoreDemo.ControlStructures
{
    public class ScheduleWorkflow : IWorkflow
    {
        public string Id => "ScheduleWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith(context => Console.WriteLine("开始"))
                    .Schedule(data => TimeSpan.FromSeconds(5)).Do(schedule => schedule
                    .StartWith(context => Console.WriteLine("后台工作")))
                .Then(context => Console.WriteLine("前台工作"));
        }
    }
}
