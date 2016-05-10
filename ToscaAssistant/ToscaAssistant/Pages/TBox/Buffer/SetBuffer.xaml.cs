using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToscaAssistant.Pages.TBox.Buffer
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            

            Dictionary<string, TestStepValueData> testStepValueDatas = new Dictionary<string, TestStepValueData>();
            TestStepValueData bufferNameTSV = new TestStepValueData(BufferName.Text, BufferValue.Text, TestStepValueData.ActionModes.Input);
            testStepValueDatas.Add(BufferName.Text,bufferNameTSV);
            
            App.AssistantMain.TestDataHandler.CreateTestStepFromSET("SetBuffer", testStepValueDatas);
        }
    }
}
