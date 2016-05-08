using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ToscaAssistant.Pages.Windows.Operations
{
    /// <summary>
    /// Interaction logic for TakeScreenshot.xaml
    /// </summary>


    public class DataObject
    {
        public IList<string> ActionModes { get; set; }

        public DataObject()
        {
            ActionModes = new List<string> {"Save", "Verify", "Buffer"};
        }

    }

    public partial class TakeScreenshot : UserControl
    {
        public TakeScreenshot()
        {
            InitializeComponent();
            DataContext = new DataObject();
            


        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog {IsFolderPicker = true};
             var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ((TextBox) sender).Text = dialog.FileName;
            }
        }

        private void SplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
