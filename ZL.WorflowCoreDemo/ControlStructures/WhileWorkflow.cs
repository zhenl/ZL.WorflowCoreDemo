using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.ControlStructures
{
    public class WhileWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "While";
        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {
            builder
                .StartWith<HelloWithName>()
                    .Input(step => step.Name, data => data.MyName)
                .While(data => data.MyName.Length > 1)
                    .Do(x => x
                        .StartWith<HelloWithName>()
                            .Input(step => step.Name, data => data.MyName)
                        .Then((data,context)=> { d})
                            .Input(step => step.Value1, data => data.Counter)
                            .Output(data => data.Counter, step => step.Value2))
                
        }
    }
}
