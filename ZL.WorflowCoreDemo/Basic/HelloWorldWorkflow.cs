using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.Basic.Steps;

namespace ZL.WorflowCoreDemo.Basic
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorld";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<GoodbyeWorld>();
        }
    }
}
