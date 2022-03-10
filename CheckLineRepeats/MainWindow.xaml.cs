using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;

namespace CheckLineRepeats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearOutputBox()
        {
            outputTextBox.Text = string.Empty;
        }

        private void OpenFile_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "test";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                ClearOutputBox();

                string filename = dialog.FileName;
                List<string> lines = new List<string>();

                using (var sr = new StreamReader(filename))
                {
                    var readLine = sr.ReadLine();
                    while (readLine != null)
                    {
                        lines.Add(readLine);
                        readLine = sr.ReadLine();
                    }
                }
                // list with line + it's count
                var duplicates = lines.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => new {Element = y.Key, Counter = y.Count()}).ToList();

                for(int i = 0; i < duplicates.Count(); i++)
                {
                    outputTextBox.Text += duplicates[i].Element + "\t\t\t" + duplicates[i].Counter + "\n";
                }
            }
        }
    }
}
