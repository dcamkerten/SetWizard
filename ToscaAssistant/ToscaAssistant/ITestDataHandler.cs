using System.Collections.Generic;

namespace ToscaAssistant
{
    public interface ITestDataHandler
    {
        void CreateTestStepFromSET(string setName, Dictionary<string, TestStepValueData> testStepValues);
    }
}