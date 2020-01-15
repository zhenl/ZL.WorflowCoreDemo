using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.InputDataToStep;
namespace ZL.WorflowCoreDemo.ControlStructures
{
    class FlowRunDecision
    {
        public static void Run()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<DecisionWorkflow, MyNameClass>();

            host.Start();

            var myClass = new MyNameClass { MyName = "张三" };

            host.StartWorkflow("DecisionWorkflow", 1, myClass);

            var activity = host.GetPendingActivity("activity-1", "worker1", TimeSpan.FromMinutes(1)).Result;


            if (activity != null)
            {
                Console.WriteLine("输入名字");
                string value = Console.ReadLine();
                host.SubmitActivitySuccess(activity.Token, value);
                
            }

            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
