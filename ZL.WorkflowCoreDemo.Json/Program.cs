using Microsoft.Extensions.DependencyInjection;
using System;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace ZL.WorkflowCoreDemo.Json
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            var loader = serviceProvider.GetService<IDefinitionLoader>();

            //var json =  "{" +
            //    "  \"Id\": \"HelloWorld\",  " +
            //    "\"Version\": 1,  \"Steps\": " +
            //    "[    {      \"Id\": \"Hello\"," +
            //    "      \"StepType\": \"ZL.WorflowCoreDemo.Basic.Steps.HelloWorld,ZL.WorflowCoreDemo\"," +
            //    "      \"NextStepId\": \"Bye\"" +
            //    "    },     " +
            //    "{      \"Id\": \"Bye\"," +
            //    "      \"StepType\": \"ZL.WorflowCoreDemo.Basic.Steps.GoodbyeWorld,ZL.WorflowCoreDemo\"" +
            //    "    }  ]}";
            var json = System.IO.File.ReadAllText("myflow.json");
            loader.LoadDefinition(json, Deserializers.Json);
            
            var host = serviceProvider.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<HelloWorldWorkflow>();
            host.Start();
            host.StartWorkflow("HelloWorld", 1, null);
            
            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            services.AddWorkflowDSL();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
