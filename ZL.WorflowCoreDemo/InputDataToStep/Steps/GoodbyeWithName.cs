using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace ZL.WorflowCoreDemo.InputDataToStep.Steps
{
    public class GoodbyeWithName : StepBody
    {
        public string Name { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(Name + ",再见");
            return ExecutionResult.Next();
        }
    }
}
