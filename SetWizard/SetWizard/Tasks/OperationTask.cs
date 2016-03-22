using SetWizard.internalObjects;
using System;
using System.Threading;
using System.Windows.Threading;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;


namespace SetWizard.Tasks
{

    class OperationTask : TCAddOnTask
    {
        public override Type ApplicableType => typeof(TCComponentFolder);


        public override string Name => "New Operation";


        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            var currentFolder = objectToExecuteOn as TCComponentFolder;

            var newWindowThread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher));

                var preferencesWindow = new MainWindow();

                preferencesWindow.Closed += (s, e) =>
                    Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);

                preferencesWindow.Show();
                Dispatcher.Run();
            });

            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
            newWindowThread.Join();


            var operation = "TBoxClipboard";

            var newTestCaseFolder=currentFolder.CreateTestCasesFolder();
            var testCase= newTestCaseFolder.CreateTestCase();
            testCase.Name = operation;
            var query= "->PROJECT->AllOwnedSubItems=>SUBPARTS: XModule[SetWizard ==\"" + operation + "\"]";
            XModule xModule = objectToExecuteOn.Search(query)[0] as XModule;

            var currentTestStep = testCase.CreateXTestStepFromXModule(xModule).TestStepValues;

            switch (operation)
            {
                case "TBoxClipboard":
                     new Clipboard().setupTestSteps(currentTestStep);
                    break;
            }

            return null;
        }
    }
}
