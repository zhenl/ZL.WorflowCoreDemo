using System;
using System.Collections.Generic;

using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace ZL.WorflowCoreDemo.InputDataToStep
{
    public class FlowRunDynamic
    {
        public static void Run()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            
            host.RegisterWorkflow<HelloWithNameWorkflowDynamic, Dictionary<string,string>>();
            host.Start();

            var initialData = new Dictionary<string,string>();
            var workflowId = host.StartWorkflow("HelloWithNameWorkflowDynamic", 1, initialData).Result;
            
            Console.WriteLine("输入名字");
            string value = Console.ReadLine();
            host.PublishEvent("MyEvent", workflowId, value);

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
