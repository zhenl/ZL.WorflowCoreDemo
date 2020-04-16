using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;
using ZL.WorflowCoreDemo.InputDictionary;

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

            var json1 = System.IO.File.ReadAllText("myflowdynamic.json");
 
            var xx = Deserializers.Json(json1);

            //Console.WriteLine(new Dictionary<string, object>() is IDictionary<string, object>);
            loader.LoadDefinition(json1, Deserializers.Json);

            var data = new ManualWorkflowData();
            data.MyDic.Add("Name", "zzd");
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.OnStepError += Host_OnStepError;
            host.OnLifeCycleEvent += (evt=> {
                Console.WriteLine(evt.ToString());
                Console.WriteLine(data.Name);
            } );
            //host.RegisterWorkflow<HelloWorldWorkflow>();
            host.Start();
            //var workflowId=host.StartWorkflow("HelloWorld", 1, null).Result;
            //var ins = host.PersistenceStore.GetWorkflowInstance(workflowId).Result;
            
          
            //var str = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            //var data1= Newtonsoft.Json.JsonConvert.DeserializeObject<ManualWorkflowData>(str);
            //Console.WriteLine(data1.MyDic["Name"]);

            //Console.WriteLine(str);
            data.Name = "gxy";
            var res=host.StartWorkflow("ManualWorkflow", data, null).Result;
            Console.WriteLine(res);
            //Console.WriteLine(data.Name);
            Console.ReadLine();
            host.Stop();
        }

        private static void Host_OnLifeCycleEvent(WorkflowCore.Models.LifeCycleEvents.LifeCycleEvent evt)
        {
            
            Console.WriteLine(evt.ToString());
        }

        private static void Host_OnStepError(WorkflowCore.Models.WorkflowInstance workflow, WorkflowCore.Models.WorkflowStep step, Exception exception)
        {
            
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
