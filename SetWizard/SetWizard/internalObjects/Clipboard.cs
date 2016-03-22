using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAPIObjects.Objects;

namespace SetWizard.internalObjects
{
    class Clipboard
    {
        public void setupTestSteps(IEnumerable<XTestStepValue> testStepValues)
        {
            foreach (var value in testStepValues)
            {
                if (value.Name == "Value") value.Value = "dddd";
            }
        }

    }
}
