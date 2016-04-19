using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ToscaAssistant.Pages.Windows.Operations
{
    /// <summary>
    /// Interaction logic for TakeScreenshot.xaml
    /// </summary>
    public partial class TakeScreenshot : UserControl
    {
        public TakeScreenshot()
        {
            InitializeComponent();
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
    }
}
