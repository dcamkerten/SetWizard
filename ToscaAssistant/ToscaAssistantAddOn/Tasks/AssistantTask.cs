using System;
using System.Collections.Generic;
using System.Threading;
using ToscaAssistant;
//using System.Windows.Threading;
//using ToscaAssistant;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaAssistantAddOn.Tasks
{
    public class AssistantTask : TCAddOnTask
    {
        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            ITestDataHandler testDataHandler = new TestDataHandler.ToscaTestDataHandler(objectToExecuteOn);
            AssistantMain assistant = new AssistantMain(testDataHandler);
            assistant.StartMainWindow();
            return null;
        }

        public override string Name => "Assistant";
        public override Type ApplicableType => typeof (TestStepFolder);
    }
}