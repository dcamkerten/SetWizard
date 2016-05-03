using System.Threading;
using System.Windows.Threading;
using ToscaAssistant;
using Tricentis.TCCore.BusinessObjects.Testcases;
using Tricentis.TCCore.Persistency;
using Tricentis.TCCore.Persistency.Tasks;
//using ToscaAssistant;

namespace ToscaAssistantAddIn.Tasks
{

    public class AssistantTask : ThreadTask {
        public override TaskGroup Group => TaskGroup.Default;
        //public override TaskCategory Category
        //{
        //    get
        //    {
        //        return AssistantAddIn.Category;
        //    }
        //}

        public static bool IsPossibleForObject(PersistableObject obj) {
            TestStepFolder teststepFolder = obj as TestStepFolder;
            if (teststepFolder != null) return true;
            return false;
        }

        public override object Execute(PersistableObject obj, ITaskContext context)
        {
            //Open Assistant Window (in another thread?)
            //MainWindow asd = new ToscaAssistant.MainWindow();
            return null;
        }

        protected override void RunInMainThread()
        {
            AssistantMain assistantMain = new AssistantMain();
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
        }

        protected override void RunInObserverThread()   
        {
            //throw new NotImplementedException();
        }

        public override string Name => "ToscaAssistant";
    }
}