using Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsaApp.Services
{
    public interface IWorkflowService
    {
        Task<WorkflowStatus> StartWorkFlow<T>(string workflowName, T dto);
    }
}
