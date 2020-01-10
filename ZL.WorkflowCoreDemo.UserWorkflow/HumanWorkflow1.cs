using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace ZL.WorkflowCoreDemo.UserWorkflow
{
    public class HumanWorkflow1 : IWorkflow
    {
        public string Id => "HumanWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
            .StartWith(context => Console.WriteLine("start"))
                .UserTask("Do you approve", data => "MYDOMAIN\\zzd")
                    .WithOption("yes", "I approve").Do(then => then
                        .StartWith(context => Console.WriteLine("You approved"))
                    )
                    .WithOption("no", "I do not approve").Do(then => then
                        .StartWith(context => Console.WriteLine("You did not approve"))
                    )
                .Then(context => Console.WriteLine("end"));
        }
    }
}
