using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Accelerator.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Audio_VOA_Index_Page : Page
    {
        public Audio_VOA_Index_Page()
        {
            this.InitializeComponent();
            VOA_L_Frame.Navigate(typeof(Audio_VOAL_Page));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((PivotItem)(((Pivot)sender).SelectedItem)).Header.ToString())
            {
                case "慢速":
                    VOA_L_Frame. Navigate(typeof(Audio_VOAL_Page));
                    break;
                case "常速":
                    VOA_N_Frame.Navigate(typeof(Audio_VOAN_Page));
                    break;
            }
        }
    }
}
