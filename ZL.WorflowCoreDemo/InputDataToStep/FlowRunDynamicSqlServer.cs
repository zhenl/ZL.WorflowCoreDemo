using System;
using System.Collections.Generic;

using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace ZL.WorflowCoreDemo.InputDataToStep
{
    public class FlowRunDynamicSqlServer
    {
        public static void Run()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            
            host.RegisterWorkflow<HelloWithNameWorkflowDynamic, Dictionary<string,string>>();
            host.Start();

            var initialData = new Dictionary<string,string>();
            Console.WriteLine("请输入需要恢复的流程编号，如执行新流程直接回车：");
            string workflowId = Console.ReadLine();

            if (string.IsNullOrEmpty(workflowId))
            {
                workflowId = host.StartWorkflow("HelloWithNameWorkflowDynamic", 1, initialData).Result;
                Console.WriteLine(workflowId);
            }
            else
            {
                try
                {
                    var success = host.ResumeWorkflow(workflowId).Result;
                    if (!success)
                    {
                        Console.WriteLine("失败");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }

            Console.WriteLine("输入名字");
            string value = Console.ReadLine();
            host.PublishEvent("MyEvent", workflowId, value);

            
            Console.ReadLine();
            foreach (var key in initialData.Keys)
            {
                Console.WriteLine(key + ":" + initialData[key]);
            }
            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow(x => x.UseSqlServer(@"Server=.;Database=WorkflowCore3;Trusted_Connection=True;", true, true));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
