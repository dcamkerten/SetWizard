using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToscaAssistantAddIn.Tasks;
using Tricentis.TCCore.BusinessObjects.Folders;
using Tricentis.TCCore.BusinessObjects.Testcases;
using Tricentis.TCCore.Persistency;
using Tricentis.TCCore.Persistency.AddInManager;
using Task = Tricentis.TCCore.Persistency.Task;
using TaskFactory = Tricentis.TCCore.Persistency.TaskFactory;

namespace ToscaAssistantAddIn.TCAddIns
{
    public class TestStepFolderTaskInterceptor : TaskInterceptor
    {
        public TestStepFolderTaskInterceptor (TestStepFolder folder) { }
        public override void GetTasks(PersistableObject obj, List<Task> tasks)
        {
            if (!AssistantTask.IsPossibleForObject(obj)) { return; }
            tasks.Add(TaskFactory.Instance.GetTask(typeof(AssistantTask)));
        }
    }
    internal class TCFolderTaskInterceptor : TaskInterceptor
    {
        #region Constructors and Destructors

        public TCFolderTaskInterceptor(TCFolder folder)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override void GetTasks(PersistableObject obj, List<Task> tasks)
        {
     
        }

        #endregion
    }
}
