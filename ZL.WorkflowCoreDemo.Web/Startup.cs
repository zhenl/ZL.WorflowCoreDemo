using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace ZL.WorkflowCoreDemo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddLogging();
            services.AddWorkflow();

            services.AddWorkflowDSL();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            var loader = app.ApplicationServices.GetService<IDefinitionLoader>();

            var json = System.IO.File.ReadAllText("myflow.json");
            loader.LoadDefinition(json, Deserializers.Json);

            var json1 = System.IO.File.ReadAllText("myflowdynamic.json");

            var xx = Deserializers.Json(json1);

            loader.LoadDefinition(json1, Deserializers.Json);

            host.Start();
        }
    }
}
