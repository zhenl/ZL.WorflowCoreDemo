using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;
using ZL.WorflowCoreDemo.InputDictionary;

namespace ZL.WorkflowCoreDemo.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWorkflowHost host;

        public string Name { get; set; }
        public IndexModel(ILogger<IndexModel> logger,IWorkflowHost host)
        {
            _logger = logger;
            this.host = host;
        }

        public void OnGet()
        {
            var data = new ManualWorkflowData();
            data.MyDic.Add("Name", "zzd");

            host.OnLifeCycleEvent += (evt => {
                Name = data.Name;
            });

            data.Name = "gxy";
            var res = host.StartWorkflow("ManualWorkflow", data, null).Result;

            Thread.Sleep(1000);
        }

        
    }
}
