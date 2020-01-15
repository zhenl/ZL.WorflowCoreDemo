using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using ZL.WorflowCoreDemo.InputDataToStep;

namespace ZL.WorflowCoreDemo.Paralle
{
    public class FlowRun
    {
        public static void Run()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<ParalleWorkflow>();

            host.Start();
            host.OnStepError += Host_OnStepError;
            host.StartWorkflow("ParalleWorkflow", 1, null);


            Console.ReadLine();
            host.Stop();
        }

        private static void Host_OnStepError(WorkflowCore.Models.WorkflowInstance workflow, WorkflowCore.Models.WorkflowStep step, Exception exception)
        {
            Console.WriteLine(exception.Message);
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
