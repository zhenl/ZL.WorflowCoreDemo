using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

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
