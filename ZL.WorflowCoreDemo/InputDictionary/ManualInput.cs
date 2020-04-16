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
        public string Name { get; set; }

        public StepPara stepPara { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(Name);
            if(stepPara != null)
            {
                Console.WriteLine(stepPara.ParaValue);
            }
            if (Paras != null)
            {
                foreach (var p in Paras)
                {
                    Console.WriteLine(p.Key + ":" + p.Value);
                }
                Paras.Add(Paras.Count.ToString(), Paras.Count);
            }
            else
            {
                Console.WriteLine("No paras");
            }

            Name = "你好" + Name;
            
             return ExecutionResult.Next();
        }
    }
}
