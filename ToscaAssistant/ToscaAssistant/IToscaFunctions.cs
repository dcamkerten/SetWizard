using System.Collections.Generic;

namespace ToscaAssistant
{
    public interface IToscaFunctions
    {
        void CreateTestStepFromSET(string setName, Dictionary<string, TestStepValueData> testStepValues);
    }
}