using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int counter = 0;
        TreeViewUC treev;
        PluginView plugv;
        public MainWindow()
        {
            InitializeComponent();
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(50, 150, 150));
            Background = b;
            treev = new TreeViewUC(ListView1);
            this.ContentControl.Content = treev;
            plugv = new PluginView(this);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            TabItem tab = new TabItem();
            tab.Header = "New File " + counter.ToString();
            tab.HeaderTemplate = TabControl.FindResource("TabHeader") as DataTemplate;
            tab.Content = new RichTextBox();
            ((RichTextBox)tab.Content).TextChanged += new TextChangedEventHandler(Tabtext_Changed);
            tab.Tag = "";
            tab.IsSelected = true;
            TabControl.Items.Add(tab);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RichTextBox rtb = (RichTextBox)((TabItem)TabControl.SelectedItem).Content;
                string text = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
                if (!String.IsNullOrWhiteSpace(text))
                {
                    Trace.WriteLine("not empty");
                    SaveFileDialog savefileDialog = new SaveFileDialog();
                    savefileDialog.DefaultExt = ".txt";
                    savefileDialog.Filter = "TXT documents (.txt) |*.txt";
                    savefileDialog.FileName = (string)((TabItem)TabControl.SelectedItem).Header;
                    Nullable<bool> result = savefileDialog.ShowDialog();
                    if (result == true)
                    {
                        File.WriteAllText(savefileDialog.FileName, text);
                        ((TabItem)TabControl.SelectedItem).Tag = "";
                    }
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
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

                fname = fname.Split(@"\"[0]).Last();

                TabItem tab = new TabItem();
                if (fname.Length > 30)
                    fname = fname.Substring(0, 31) + "...";
                tab.Header = fname;
                tab.HeaderTemplate = TabControl.FindResource("TabHeader") as DataTemplate;
                tab.Content = rtb;
                ((RichTextBox)tab.Content).TextChanged += new TextChangedEventHandler(Tabtext_Changed);
                tab.Tag = "";
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

            DirectoryInfo dinfo;
            FileInfo[] Files;
            try
            {
                dinfo = new DirectoryInfo(path);
                Files = dinfo.GetFiles("*.txt");
            }
            catch (ArgumentException)
            {
                Files = new FileInfo[0];
            }

            foreach (FileInfo file in Files)
                ListView1.Items.Add(file);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Text editor application\nAllows to: create and save files, open files from folder and display all txt files in selected folder.\nTwo text-editing plugins available\nTo start work go to File->New", "About");
        }

        private void Open_file_folder(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            string fname = ((FileInfo)item).FullName;

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

            fname = ((FileInfo)item).Name;
            TabItem tab = new TabItem();
            if (fname.Length > 30)
                fname = fname.Substring(0, 31) + "...";
            tab.Header = fname;
            tab.HeaderTemplate = TabControl.FindResource("TabHeader") as DataTemplate;
            tab.Content = rtb;
            ((RichTextBox)tab.Content).TextChanged += new TextChangedEventHandler(Tabtext_Changed);
            tab.Tag = "";
            tab.IsSelected = true;

            TabControl.Items.Add(tab);
        }
        
        private void Tabtext_Changed(object sender, RoutedEventArgs e)
        {
            ((TabItem)((FrameworkElement)e.Source).Parent).Tag = "1";
        }

        private void CloseTab(object sender, RoutedEventArgs e)
        {
            TabItem tab = new TabItem();
            foreach (TabItem t in TabControl.Items)
            {
                if (t.Header.Equals(((Button)sender).Tag))
                {
                    tab = t;
                    break;
                }
            }
            if (tab != null)
            {
                if ((string)tab.Tag == "1")
                    if (MessageBox.Show("Do you want to close unsaved document?", "Close document", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                if (TabControl.SelectedItem == tab)
                {
                    if(tab.TabIndex>0) TabControl.SelectedItem = tab.TabIndex - 1;
                    else if(TabControl.Items.Count>1) TabControl.SelectedItem = tab.TabIndex + 1;
                }

                if (TabControl != null)
                    TabControl.Items.Remove(tab);
            }
        }

        private void Tree_check(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ContentControl.Content = treev;
            }
            catch (NullReferenceException) { }
        }

        private void Plugin_check(object sender, RoutedEventArgs e)
        {
            this.ContentControl.Content = plugv;
        }

        public void Split_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedItem != null)
            {
                SplitLetters sl = new SplitLetters();
                RichTextBox rtb = ((RichTextBox)((TabItem)TabControl.SelectedItem).Content);
                TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                Trace.WriteLine(tr.Text);
                FlowDocument myFlowDoc = new FlowDocument();
                myFlowDoc.Blocks.Add(new Paragraph(new Run(sl.Do(tr.Text))));
                rtb.Document = myFlowDoc;
            }
        }

        public void Upper_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedItem != null)
            {
                Uppercase up = new Uppercase();
                RichTextBox rtb = ((RichTextBox)((TabItem)TabControl.SelectedItem).Content);
                TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                Trace.WriteLine(tr.Text);
                FlowDocument myFlowDoc = new FlowDocument();
                myFlowDoc.Blocks.Add(new Paragraph(new Run(up.Do(tr.Text))));
                rtb.Document = myFlowDoc;
            }
        }
    }
}
