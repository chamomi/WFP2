using System.Windows;
using System.Windows.Controls;

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for PluginView.xaml
    /// </summary>
    public partial class PluginView : UserControl
    {
        MainWindow parent;
        public PluginView(MainWindow p)
        {
            InitializeComponent();
            parent = p;
        }

        private void Plugin_Click(object sender, RoutedEventArgs e)
        {
            if (parent.TabControl.SelectedItem != null)
            {
                if (CBox.SelectedIndex == 0) parent.Sp.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
                else parent.Up.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            }
        }
    }
}
