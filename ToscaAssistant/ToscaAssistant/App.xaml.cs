using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToscaAssistant.TestDataHandler;

namespace ToscaAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static AssistantMain assistantMain = new AssistantMain(new StandAloneTestDataHandler());

        public static AssistantMain AssistantMain
        {
            get { return assistantMain; }
        }

        public App(AssistantMain assistant):base()
        {
            assistantMain = assistant;
        }
    }
}
