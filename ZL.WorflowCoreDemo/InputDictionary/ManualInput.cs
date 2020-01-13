using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ZL.WorflowCoreDemo.InputDictionary
{
    public class ManualInput : StepBody
    {
        public Dictionary<string, object> Paras { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            if (Paras != null)
            {
                foreach (var p in Paras)
                {
                    Console.WriteLine(p.Key + ":" + p.Value);
                }
            }
            else
            {
                Console.WriteLine("No paras");
            }
            
             return ExecutionResult.Next();
        }
    }
}
