﻿using System;
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
    public sealed partial class Audio_BBC_Index_Page : Page
    {
        public Audio_BBC_Index_Page()
        {
            this.InitializeComponent();
            //BBC6_Frame.Navigate(typeof(Audio_BBC6_Page));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((PivotItem)(((Pivot)sender).SelectedItem)).Header.ToString())
            {
                case "BBC六分钟":
                    //BBC6_Frame.Navigate(typeof(Audio_BBC6_Page));
                    break;
                case "BBC新闻":
                    //BBCNews_Frame.Navigate(typeof(Audio_BBCNews_Page));
                    break;
                case "BBC职场英语":
                    //BBCOffice_Frame.Navigate(typeof(Audio_BBCOffice_Page));
                    break;
                case "BBC地道英语":
                    //BBCNative_Frame.Navigate(typeof(Audio_BBCNative_Page));
                    break;
            }
        }
    }
}
