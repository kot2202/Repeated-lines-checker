using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace CheckLineRepeats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Media.SolidColorBrush successBrush = System.Windows.Media.Brushes.LightGreen;
        private System.Windows.Media.SolidColorBrush failBrush = System.Windows.Media.Brushes.Red;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt|All files (*.*)|*.*";

            bool? result = dialog.ShowDialog();
            if (result == true)
            {

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
                // ListView populating seems to not work with objects for some reason
                //var duplicates = lines.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => new {Element = y.Key, Counter = y.Count()}).ToList();

                // list with line + it's count
                var duplicates = lines.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => new Line { Text = y.Key, Count = y.Count() }).ToList();
                mainListView.ItemsSource = duplicates;
                UpdateView(duplicates.Count);
            }
        }

        private void OpenFile_Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }

        private void UpdateView(int count)
        {
            if (count > 0)
            {
                outputTextBlock.Background = failBrush;
                outputTextBlock.Text = $"{count} duplicates need to be solved!";
            }
            else
            {
                outputTextBlock.Background = successBrush;
                outputTextBlock.Text = "No duplicates found.";
            }
        }
    }
}
