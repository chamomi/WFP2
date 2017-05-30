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
        public PluginView()
        {
            InitializeComponent();
        }

        //public async void FadeOut()
        //{
        //    DoubleAnimation ani = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
        //    this.BeginAnimation(PluginView.OpacityProperty, ani);
        //}

        //public async void FadeIn(TreeView tv)
        //{
        //    DoubleAnimation ani = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
        //    DoubleAnimation ani1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
        //    tv.BeginAnimation(TreeView.OpacityProperty, ani1);
        //    this.BeginAnimation(PluginView.OpacityProperty, ani);
        //}
    }
}
