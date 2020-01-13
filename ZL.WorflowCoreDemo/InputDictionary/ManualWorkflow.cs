using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ZL.WorflowCoreDemo.InputDictionary
{
    public class ManualWorkflow : IWorkflow<Dictionary<string,object>>
    {
        public string Id => "ManualWorkflow";
        public int Version => 1;
        
        public void Build(IWorkflowBuilder<Dictionary<string, object>> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())
                .WaitFor("MyEvent", (data, context) => context.Workflow.Id, data => DateTime.Now)
                    .Output((step, data)=> {
                        //data = step.EventData as Dictionary<string, string>;
                        var dic = step.EventData as Dictionary<string, object>;
                        foreach (var key in dic.Keys)
                        {
                            if (data.ContainsKey(key)) data[key] = dic[key];
                            else data.Add(key, dic[key]);
                        }
                    }).Then<ManualInput>()
                       .Input((step,data)=>
                       {
                           step.Paras = data;
                           Console.WriteLine(data.Count);
                       });
                
        }
    }
}
