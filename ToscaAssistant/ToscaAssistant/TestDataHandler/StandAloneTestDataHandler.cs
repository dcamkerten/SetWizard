using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToscaAssistant.TestDataHandler
{
    class StandAloneTestDataHandler : ITestDataHandler
    {
        public StandAloneTestDataHandler()
        {
            
        }
        public void CreateTestStepFromSET(string setName, Dictionary<string, TestStepValueData> testStepValues)
        {
            string teststeps="";
            foreach (KeyValuePair<string, TestStepValueData> testSTepVAluePair in testStepValues)
            {
                teststeps += "- Name: \"" + testSTepVAluePair.Value.Name + "\","
                             + "Value: \"" + testSTepVAluePair.Value.Value + "\","
                             + "ActionMode: \"" + testSTepVAluePair.Value.ActionMode.ToString() + "\"\n";
            }

            MessageBox.Show("<Stub>\nCreate TestStep \"" + setName+"\"\nand TestStepValues:\n" + teststeps);
        }
    }
}
