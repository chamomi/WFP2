using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for TreeViewUC.xaml
    /// </summary>
    public partial class TreeViewUC : UserControl
    {
        ListView lv = new ListView();
        int count = 0;
        public TreeViewUC(ListView _lv)
        {
            InitializeComponent();
            lv = _lv;
        }

        private void Populate(string header, string tag, TreeView _root, TreeViewItem _child, bool isfile)
        {
            TreeViewItem _driitem = new TreeViewItem();
            _driitem.Tag = tag;
            _driitem.Header = header;
            _driitem.Expanded += new RoutedEventHandler(_driitem_Expanded);
            if (!isfile)
                _driitem.Items.Add(new TreeViewItem());

            if (_root != null)
            { _root.Items.Add(_driitem); }
            else { _child.Items.Add(_driitem); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                foreach (DriveInfo driv in DriveInfo.GetDrives())
                {
                    if (driv.IsReady)
                        Populate(driv.Name, driv.Name, folders, null, false);
                }
                count++;
            }
        }


        void _driitem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem _item = (TreeViewItem)sender;
            if (_item.Items.Count == 1 && ((TreeViewItem)_item.Items[0]).Header == null)
            {
                _item.Items.Clear();
                try
                {
                    foreach (string dir in Directory.GetDirectories(_item.Tag.ToString()))
                    {
                        DirectoryInfo _dirinfo = new DirectoryInfo(dir);
                        Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, false);
                    }

                    foreach (string dir in Directory.GetFiles(_item.Tag.ToString()))
                    {
                        FileInfo _dirinfo = new FileInfo(dir);
                        Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, true);
                    }
                }
                catch(UnauthorizedAccessException)
                { MessageBox.Show("You have no access to these files.", "Access denied"); return; }
            }
        }

        static TreeViewItem GetParentItem(TreeViewItem item)
        {
            for (var i = VisualTreeHelper.GetParent(item); i != null; i = VisualTreeHelper.GetParent(i))
                if (i is TreeViewItem)
                    return (TreeViewItem)i;

            return null;
        }

        private void folders_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            lv.Items.Clear();

            var path = Convert.ToString(((TreeViewItem)folders.SelectedItem).Header);

            for (var i = GetParentItem((TreeViewItem)folders.SelectedItem); i != null; i = GetParentItem(i))
                path = i.Header + @"\\" + path;

            DirectoryInfo dinfo;
            FileInfo[] Files = { new FileInfo("./") };
            try
            {
                dinfo = new DirectoryInfo(path);
                try { Files = dinfo.GetFiles("*.txt"); }
                catch(UnauthorizedAccessException)
                { MessageBox.Show("You have no access to these files.", "Access denied"); return; }
            }
            catch (ArgumentException)
            {
                Files = new FileInfo[0];
            }

            foreach (FileInfo file in Files)
                lv.Items.Add(file);
        }
    }
}
