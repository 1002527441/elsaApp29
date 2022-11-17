using ElsaApp.Data;
using ElsaApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsaApp.Pages
{
    public partial class Counter
    {      
        private int currentCount = 0;

        [Inject]
        public IWorkflowService workflowService { get; set; }

        private void IncrementCount()
        {
            currentCount++;
        }

        private void StartWorkflow()
        {
            var hello = new DTOHello();
            hello.Id = Guid.NewGuid().ToString();
            hello.Name = "henry, 951567a";
           var result = workflowService.StartWorkFlow("Hello", hello); 
        }

    }
}
