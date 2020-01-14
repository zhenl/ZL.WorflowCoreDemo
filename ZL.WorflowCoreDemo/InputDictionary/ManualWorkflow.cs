using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ZL.WorflowCoreDemo.InputDictionary
{
    public class ManualWorkflow : IWorkflow<ManualWorkflowData>
    {
        public string Id => "ManualWorkflow";
        public int Version => 1;
        
        public void Build(IWorkflowBuilder<ManualWorkflowData> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())
                .WaitFor("MyEvent", (data, context) => context.Workflow.Id, data => DateTime.Now)
                    .Output((step, data)=> {
                        var dic = step.EventData as Dictionary<string, object>;
                        foreach (var key in dic.Keys)
                        {
                            if (data.MyDic.ContainsKey(key)) data.MyDic[key] = dic[key];
                            else data.MyDic.Add(key, dic[key]);
                        }
                    }).Then<ManualInput>()
                       .Input((step,data)=>
                       {
                           step.Name = data.Name;
                           step.Paras = data.MyDic;
                           //Console.WriteLine(data.Count);
                       });
                
        }
    }
}
