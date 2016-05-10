using System;
using System.Collections.Generic;
using System.IO;
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
            //ITestDataHandler testDataHandler = new TestDataHandler.ToscaTestDataHandler(objectToExecuteOn);
            //AssistantMain assistant = new AssistantMain(testDataHandler);
            //assistant.StartMainWindow();


            //string toscahome = Path.GetFullPath(Environment.GetEnvironmentVariable("TRICENTIS_HOME"));

            System.Diagnostics.Process.Start("c:\\Temp\\ToscaAssistant.exe");


            var currentTescase = objectToExecuteOn as TestCase;


            var query = "->PROJECT->AllOwnedSubItems=>SUBPARTS: XModule[SetWizard == \"File\"]";
            XModule xModule = objectToExecuteOn.Search(query)[0] as XModule;


          



            var currentTestStep = currentTescase.CreateXTestStepFromXModule(xModule);

            foreach (var testValue in currentTestStep.TestStepValues)
            {
                if (testValue.Name == "Directory")
                {
                    testValue.Value = "C:\\GIG";
                }


                if (testValue.Name == "File")
                {
                    testValue.Value = "createdFile.txt";
                }


                if (testValue.Name == "Text")
                {
                    testValue.Value = "Usability";
                }

            }

            foreach (var attribute in xModule.Attributes)
            {

   
                if (attribute.Name == "Encoding")
                {
                    XTestStepValue testStepValue = currentTestStep.CreateXTestStepValue(attribute);
                    testStepValue.Value = "UTF-8";
                }

                if (attribute.Name == "Overwrite")
                {
                    XTestStepValue testStepValue = currentTestStep.CreateXTestStepValue(attribute);
                    testStepValue.Value = "True";
                }
                


            }



            return null;
        }

        public override string Name => "Assistant";
        public override Type ApplicableType => typeof (TestCase);
    }
}