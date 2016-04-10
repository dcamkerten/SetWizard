using System;
using System.Threading;
using System.Windows.Threading;
using ToscaAssistant;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaAssistantAddOn.Tasks
{
    public class AssistantTask : TCAddOnTask
    {
        public override TCObject Execute(TCObject objectToExecuteOn,
            TCAddOnTaskContext taskContext)
        {
            //Open Assistant Window (in another thread?)
            //Open Assistant Window (in another thread?)
            var newWindowThread = new Thread(() =>
            {
                MainWindow assistantWindow = new ToscaAssistant.MainWindow();
                assistantWindow.Closed += (s, e) =>
                    Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);

                assistantWindow.Show();
                Dispatcher.Run();
            });

            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
            return null;
        }

        public override string Name => "Assistant";
        public override Type ApplicableType => typeof (TestStepFolder);
    }
}