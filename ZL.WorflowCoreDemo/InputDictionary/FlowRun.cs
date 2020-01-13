using System;
using System.Collections.Generic;

using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace ZL.WorflowCoreDemo.InputDictionary
{
    public class FlowRun
    {
        public static void Run()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();

            host.RegisterWorkflow<ManualWorkflow, ManualWorkflowData>();
            host.Start();

            var initialData = new ManualWorkflowData();
            var workflowId = host.StartWorkflow("ManualWorkflow", 1, initialData).Result;

            Console.WriteLine("输入名字");
            string value = Console.ReadLine();
            var dic = new Dictionary<string, object>();
            dic.Add("Name", value);
            host.PublishEvent("MyEvent", workflowId, dic);

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
