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
        public AssistantMain(ITestDataHandler testDataHandler)
        {
            TestDataHandler = testDataHandler;
        }

        ~AssistantMain()
        {
            TestDataHandler = null;
        }

        public ITestDataHandler TestDataHandler { get; private set; }

        //void Start()
        //{
        //    StartMainWindow();
        //    //TODO: Get TestStep From MainWindow
        //    AssistantTask.CreateTestStep();
        //}

        public void StartMainWindow()
        {
            //Open Assistant Window(in another thread ?)
            Thread newWindowThread = new Thread(() =>
            {
                App assistantWindow = new App(this);
                
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