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
using System.Windows.Media.Animation;

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
            if (CBox.SelectedIndex == 0) parent.Sp.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            else parent.Up.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
        }
    }
}
