using System;
using System.Collections.Generic;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace ZL.WorflowCoreDemo.InputDataToStep.Steps
{
    public class HelloWithName : StepBody
    {
        public string Name { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            
            Console.WriteLine("你好," + Name);
            return ExecutionResult.Next();
        }
    }
}
