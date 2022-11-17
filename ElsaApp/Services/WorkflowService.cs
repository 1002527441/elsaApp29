using Elsa;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElsaApp.Services
{
    public class WorkflowService: IWorkflowService
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly IWorkflowInstanceStore _WorkflowInstanceStore;
        private readonly IWorkflowRegistry _WorkflowRegistry;
        private readonly IWorkflowFactory _WorkflowFactory;
        private readonly IWorkflowDefinitionStore _WorkflowDefinitionStore;
        private readonly IWorkflowLaunchpad _Launchpad;
        //private readonly IWorkflowRunner _WorkflowRunner;
        //private readonly IStartsWorkflow _StartWorkflow;
        //private readonly IResumesWorkflow _ResumesWorkflow;


        public WorkflowService(
            IWorkflowRegistry workflowRegistry,
            IWorkflowFactory workflowFactory,
            IWorkflowDefinitionStore workflowDefinitionStore,
            IWorkflowInstanceStore workflowInstanceStore,            
            IWorkflowLaunchpad launchpad
            )
        {   
            
            _WorkflowInstanceStore = workflowInstanceStore;
            _WorkflowRegistry = workflowRegistry;
            _WorkflowFactory = workflowFactory;
            _WorkflowDefinitionStore = workflowDefinitionStore;
            _Launchpad = launchpad;
        }


        /// <summary>
        /// Start a workflow
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<WorkflowStatus> StartWorkFlow<T>(string workflowName,  T dto)
        {

          
         
            var variables = new Variables();
            variables.Set("Key", dto);
            var input = new WorkflowInput(variables);

            var workflowDefinition = await GetWorkflowBlueprint(workflowName);

            if (workflowDefinition == null) return WorkflowStatus.Faulted;
    
            var runerResult = await _Launchpad.FindAndExecuteStartableWorkflowAsync(workflowDefinition, null, null,null,input);



            return runerResult.WorkflowInstance.WorkflowStatus;
        }




        public async Task<bool> ResumeWorkflow<T>(string CorrelationId, T data)
        {           
           
            var workflowInstance = await _WorkflowInstanceStore.FindByCorrelationIdAsync(CorrelationId);
            if (workflowInstance == null)
            {
                throw new ArgumentException("workflowInstance not found");
            }

            var input = new Variables();
            input.Set("data", data);

            var blockingActivity  = workflowInstance.BlockingActivities.FirstOrDefault();

            //var executionContext = await _ResumesWorkflow.ResumeWorkflowAsync(workflowInstance,blockingActivity.ActivityId);

            var collectworkflow = new CollectedWorkflow(workflowInstance.Id, workflowInstance, blockingActivity.ActivityId);

            var result  = await _Launchpad.ExecutePendingWorkflowAsync(collectworkflow, new WorkflowInput(input));     

            return result.Executed;


        }




        /// <summary>
        /// getworkflowDefinition
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="definitionName"></param>
        /// <returns></returns>

        //private async Task<WorkflowDefinitionVersion> GetWorkFlowDefinition<T>(T dto)
        //{
        //    var type = dto.GetType();
        //    var name = type.Name;


        //    var blueprint = await GetWorkflowBlueprint(name);

            
            
        //    _Launchpad.

            
        //   var a = _WorkflowRunner.RunWorkflowAsync(blueprint)

        //    var definitionList = await _WorkflowDefinitionStore.FindManyAsync(VersionOptions.Latest);
        //    var currentDefinition = definitionList.Where(x => x.Name == name && x.IsPublished == true)
        //        .OrderByDescending(x => x.Version).FirstOrDefault();
        //    var workflowDefinition = await _WorkflowRegistry.FindAsync(
        //        currentDefinition.DefinitionId,
        //        VersionOptions.SpecificVersion(currentDefinition.Version));
        //    return workflowDefinition;
        //}


        private async Task<IWorkflowBlueprint> GetWorkflowBlueprint(string definitionName)
        {
            var workflowBlueprint = await _WorkflowRegistry.FindByNameAsync(definitionName, VersionOptions.LatestOrPublished);
            return workflowBlueprint;
        }


        //public async Task<IActionResult> SendSignalToInstance(string workflowInstanceId, string userAction)
        //{

        //    var wfInstanceDef = await _instanceStore.FindAsync(new EntityIdSpecification<WorkflowInstance>(workflowInstanceId));
        //    var currentActivityId = wfInstanceDef.BlockingActivities.FirstOrDefault().ActivityId;

        //    await _workflowStorageService.UpdateInputAsync(wfInstanceDef, new WorkflowInput(userAction));

        //    var runres = await _wfTriggerInterruptor.InterruptActivityAsync(wfInstanceDef, currentActivityId);
        //    return Ok(runres.Executed);

        //}



    }
}
