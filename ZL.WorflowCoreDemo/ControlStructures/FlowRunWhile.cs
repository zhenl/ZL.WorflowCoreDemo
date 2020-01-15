using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.InputDataToStep;

namespace ZL.WorflowCoreDemo.ControlStructures
{
    class FlowRunWhile
    {

        public static void Run()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<WhileWorkflow, MyNameClass>();

            host.Start();

            var myClass = new MyNameClass { MyName = "张三" };

            host.StartWorkflow("WhileWorkflow", 1, myClass);

            var activity = host.GetPendingActivity("activity-1", "worker1", TimeSpan.FromMinutes(1)).Result;

            
            while (activity != null)
            {
                Console.WriteLine("输入大于3个字符的名字结束，小于3个字符的名字继续");
                string value = Console.ReadLine();
                host.SubmitActivitySuccess(activity.Token, value);
                activity = host.GetPendingActivity("activity-1", "worker1", TimeSpan.FromMinutes(1)).Result;
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
