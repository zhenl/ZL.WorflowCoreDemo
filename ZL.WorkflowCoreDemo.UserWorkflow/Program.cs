using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ZL.WorkflowCoreDemo.UserWorkflow
{
    class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            //start the workflow host
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<HumanWorkflow>();
            host.Start();


            Console.WriteLine("Starting workflow...");
            string workflowId = host.StartWorkflow("HumanWorkflow").Result;

            var timer = new Timer(new TimerCallback((state) => { PrintOptions(host, workflowId); }), null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));

            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("Open user actions are");
            var openItems = host.GetOpenUserActions(workflowId);
            //var workflow = host.PersistenceStore.GetWorkflowInstance(workflowId).Result;
            //var openItems = workflow.GetOpenUserActions();

            foreach (var item in openItems)
            {
                Console.WriteLine(item.Prompt + ", Assigned to " + item.AssignedPrincipal);
                Console.WriteLine("Options are ");
                foreach (var option in item.Options)
                {
                    Console.WriteLine(" - " + option.Key + " : " + option.Value + ", ");
                }

                //Thread.Sleep(500);

                Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Choosing " + item.Options.First().Key);

                host.PublishUserAction(openItems.First().Key, @"domain\john", item.Options.First().Value).Wait();
            }

            Console.ReadLine();
            host.Stop();
        }

        private static void PrintOptions(IWorkflowHost host, string workflowId)
        {
            var openItems = host.GetOpenUserActions(workflowId);
            foreach (var item in openItems)
            {
                Console.WriteLine(item.Prompt + ", Assigned to " + item.AssignedPrincipal);
                Console.WriteLine("Options are ");
                foreach (var option in item.Options)
                {
                    Console.WriteLine(" - " + option.Key + " : " + option.Value + ", ");
                }
            }
        }

        //static void Main(string[] args)
        //{
        //    //IServiceProvider serviceProvider = ConfigureServices();
        //    //var host = serviceProvider.GetService<IWorkflowHost>();
        //    //host.RegisterWorkflow<HumanWorkflow1>();
        //    //host.Start();
        //    //string workflowId = host.StartWorkflow("HumanWorkflow").Result;
        //    //Thread.Sleep(1000);
        //    //var openItems = host.GetOpenUserActions(workflowId);

        //    //host.PublishUserAction(openItems.First().Key, "MYDOMAIN\\zzd", "yes");
        //    //Console.ReadLine();
        //    //host.Stop();
        //}

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            //services.AddWorkflow(x => x.UseSqlServer(@"Server=.;Database=WorkflowCore3;Trusted_Connection=True;", true, true));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
