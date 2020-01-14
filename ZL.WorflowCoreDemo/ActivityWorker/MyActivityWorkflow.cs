using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.InputDataToStep;
using ZL.WorflowCoreDemo.InputDataToStep.Steps;

namespace ZL.WorflowCoreDemo.ActivityWorker
{
    public class MyActivityWorkflow : IWorkflow<MyNameClass>
    {
        public string Id => "MyActivityWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyNameClass> builder)
        {

            builder
                .StartWith<HelloWithName>().Input(data => data.Name, step => step.MyName)
                    .Activity("activity-1", (data) => data.MyName)
                        .Output(data => data.MyName, step => step.Result)
                    .Then<GoodbyeWithName>()
                        .Input(step => step.Name, data => data.MyName);
            

        }
    }
}
