//using System;
//using System.Collections.Generic;
//using ToscaAssistant;
//using Tricentis.TCAPIObjects.Objects;

////using Tricentis.TCAPIObjects.Objects;

//namespace ToscaAssistantAddOn.TestDataHandler
//{
//    internal class ToscaTestDataHandler : ITestDataHandler
//    {
//        private readonly TCObject parentObject;

//        internal ToscaTestDataHandler(TCObject parentObject)
//        {
//            this.parentObject = parentObject;
//        }

//        public void CreateTestStepFromSET(string setName, Dictionary<string, TestStepValueData> testStepValues)
//        {
//            TestStepFolder testStepFolder = parentObject as TestStepFolder;
//            if (testStepFolder == null) throw new NullReferenceException("CurrentFolder is not set");

//            string query = "->PROJECT->AllOwnedSubItems=>SUBPARTS:XModule[SetWizard ==\"" + setName + "\"]";
//            TCObject tcObject = testStepFolder.Search(query)[0];
//            XModule xModule = tcObject as XModule;

//            if (xModule == null) return;
//            XTestStep testStep = testStepFolder.CreateXTestStepFromXModule(xModule);

//            foreach (KeyValuePair<string, TestStepValueData> testStepValuePair in testStepValues)
//            {
//                foreach (XModuleAttribute xModuleAttribute in xModule.Attributes)
//                {
//                    if (xModuleAttribute.Name != testStepValuePair.Key) continue;
//                    XTestStepValue testStepValue = testStep.CreateXTestStepValue(xModuleAttribute);
//                    TestStepValueData testStepValueData = testStepValuePair.Value;

//                    testStepValue.Value = testStepValueData.Value;

//                    testStepValue.ActionMode = ConvertActionMode(testStepValueData.ActionMode);
//                }
//            }
//        }

//        private XTestStepActionMode ConvertActionMode(TestStepValueData.ActionModes actionMode)
//        {
//            switch (actionMode)
//            {
//                case TestStepValueData.ActionModes.Input:
//                    return XTestStepActionMode.Input;
//                case TestStepValueData.ActionModes.Verify:
//                    return XTestStepActionMode.Verify;
//                case TestStepValueData.ActionModes.Buffer:
//                    return XTestStepActionMode.Buffer;
//                default:
//                    throw new NotSupportedException("Actionmode not supported: " + actionMode);
//            }
//        }

//        public static void CreateTestStepFromSETTest(string setName, Dictionary<string, TestStepValueData> testStepValues)
//        {
//            setName = "TBoxClipboard";
//            testStepValues = new Dictionary<string, TestStepValueData>();

//            testStepValues.Add("Value", new TestStepValueData("Value", "testvalue", TestStepValueData.ActionModes.Input));
//            testStepValues.Add("Value", new TestStepValueData("BufferName", "blabla", TestStepValueData.ActionModes.Buffer));
//            App.AssistantMain.TestDataHandler.CreateTestStepFromSET(setName, testStepValues);
//        }

//        public static void CreateTestStep(IEnumerable<KeyValuePair<string, TestStepValueData>> teststepvalues)
//        {
//            foreach (var testStepValue in teststepvalues)
//            {
//                var moduleName = testStepValue.Key;
//            }
//        }
//    }
//}