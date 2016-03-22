using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;

namespace SetWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: MetroWindow
    {
        public ObservableCollection<string> possibleOperations { get; set; }

        public MainWindow()
        {
            possibleOperations = new ObservableCollection<string>();
            possibleOperations.Add("test");
            possibleOperations.Add("test2");
            possibleOperations.Add("test3");
            possibleOperations.Add("test4");

            DataContext = this;
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           

        }

        private void SplitButton_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
