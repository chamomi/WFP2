using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int counter = 0;
        public ObservableCollection<string> dir = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(50, 150, 150));
            Background = b;
            //ListView.ItemsSource = dir;
            
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            TabItem tab = new TabItem();
            tab.Header = "New File " + counter.ToString();
            tab.Content = new RichTextBox();
            tab.IsSelected = true;
            TabControl.Items.Add(tab);
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            var newFileDialog = new OpenFileDialog();
            newFileDialog.DefaultExt = ".txt";
            newFileDialog.Filter = "TXT documents (.txt) |*.txt";
            Nullable<bool> result = newFileDialog.ShowDialog();
            if (result == true)
            {
                string fname = newFileDialog.FileName;

                RichTextBox rtb = new RichTextBox();
                TextRange range;
                FileStream fStream;
                if (File.Exists(fname))
                {
                    range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    fStream = new FileStream(fname, FileMode.OpenOrCreate);
                    range.Load(fStream, DataFormats.Text);
                    fStream.Close();
                }

                TabItem tab = new TabItem();
                if(fname.Length>30)
                    fname = fname.Substring(0, 31) + "...";
                tab.Header = fname;
                tab.Content = rtb;
                tab.IsSelected = true;
                
                TabControl.Items.Add(tab);
            }
        }

        private void Folder_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            using (var folderDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = folderDialog.SelectedPath;
                }
            }

            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            foreach (FileInfo file in Files)
                ListView1.Items.Add(file.Name);

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Some information.");
        }
    }
}
