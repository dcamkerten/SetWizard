using System;
using System.Collections.Generic;
using Tricentis.TCCore.Persistency;
using Tricentis.TCCore.Persistency.AddInManager;

namespace ToscaAssistantAddIn
{
    public class AssistantAddIn : TCAddIn
    {
        static AssistantAddIn()
        {Category=new TaskCategory("Assistant");
        }

        public override string UniqueName => "Assistant";

        public override Dictionary<Type, object> TaskToIconDefinition
        {
            get
            {
//                var entry = new Dictionary<Type, object> {{typeof (ScrapperTask), "GUI.Images.engineering.png"}};
                //return entry;
                return new Dictionary<Type, object>();
            }
        }

        public static TaskCategory Category { get; set; }
    }
}
