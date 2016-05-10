using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ToscaAssistant
{
    public class TestStepValueData
    {
        public enum ActionModes
        {
            Input,
            Verify,
            Buffer
        }

        public string Name { get; }
        public string Value { get; }

        public TestStepValueData(string name, string value, ActionModes actionMode)
        {
            Name = name;
            Value = value;
            ActionMode = actionMode;
        }

        public ActionModes ActionMode { get; set; }


    }

    public class AssistantMain
    {
        public AssistantMain(IToscaFunctions toscaFunctions)
        {
            ToscaFunctions = toscaFunctions;
        }

        ~AssistantMain()
        {
            ToscaFunctions = null;
        }

        public static IToscaFunctions ToscaFunctions { get; private set; }

        //void Start()
        //{
        //    StartMainWindow();
        //    //TODO: Get TestStep From MainWindow
        //    AssistantTask.CreateTestStep();
        //}

        public void StartMainWindow()
        {
            //Open Assistant Window(in another thread ?)
            var newWindowThread = new Thread(() =>
            {
                var assistantWindow = new App();

                assistantWindow.Run();
                //MainWindow assistantWindow = new ToscaAssistant.MainWindow();
            });

            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
            //TODO return TestStepData?
        }
    }
}