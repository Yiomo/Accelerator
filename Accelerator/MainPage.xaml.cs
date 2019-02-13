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
using Accelerator.Pages;
using Accelerator.Functions;
using Accelerator.UserControls;
using Accelerator.Class;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.ApplicationModel.Core;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Accelerator
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (Setting.IsFirstlyRun() == true)
            {
                IniComponent();
            }
            else
            {
                Setting.SetSettingValue("RunTimes", Convert.ToInt32(Setting.GetSettingValue("RunTimes")).ToString());
            }

            SetTitleBarStyle();
            HideTitleBar();
            //RootFrame.Navigate(typeof(Audio_VOA_Index_Page));
        }

        private void IniComponent()
        {
            try
            {
                Setting.InitializeSettings();
            }
            catch (Exception e)
            {
                Ins_Error.eMessage = e.Message;
                Ins_Error.eSource = e.InnerException.ToString();
                Ins_Error.eStackTrace = e.StackTrace.ToString();

                Error_Control error_Control = new Error_Control();
                error_Control.Tapped += async (_s, _e) =>
                {
                    error_Control.Refreshing();
                    await Task.Delay(100);
                    IniComponent();
                    grid.Children.Remove(error_Control);
                };
                grid.Children.Add(error_Control);
            }
        }

        private void NaviView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            switch (item.Content)
            {
                case "BBC":
                    RootFrame.Navigate(typeof(Audio_BBC_Index_Page));
                    NaviViewTB.Text = "BBC英语";
                    break;

                case "VOA":
                    RootFrame.Navigate(typeof(Audio_VOA_Index_Page));
                    NaviViewTB.Text = "VOA英语";
                    break;

                case "Video":
                    RootFrame.Navigate(typeof(Video_Index_Page));
                    NaviViewTB.Text = "记录短片";
                    break;
            }
            if (item.Content.ToString().Contains("设置") == true)
            {
                RootFrame.Navigate(typeof(Setting_Page));
                NaviViewSI.Symbol = Symbol.Setting;
                NaviViewTB.Text = "设置";
            }
        }

        private void NavigationViewItem_Loading(FrameworkElement sender, object args)
        {
            ((NavigationViewItem)sender).IsSelected = true;
        }

        public void HideTitleBar()
        {
            //获取当前视图相关的状态栏
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            //将视图扩展到状态栏
            titleBar.ExtendViewIntoTitleBar = true;
        }

        public void SetTitleBarStyle()
        {
            //获取活动应用程序的视图状态和行为设置
            var view = ApplicationView.GetForCurrentView();

            //下面这两个是给当你不把状态栏隐藏时设置的

            //active 当前被激活时
            view.TitleBar.BackgroundColor = Colors.Red;
            view.TitleBar.ForegroundColor = Colors.Black;

            //inactive 不是当前窗口，我觉得不常用
            view.TitleBar.InactiveBackgroundColor = Colors.Red;
            view.TitleBar.InactiveForegroundColor = Colors.Black;

            //button

            //初始
            view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            view.TitleBar.ButtonForegroundColor = Colors.White;

            //悬浮
            view.TitleBar.ButtonHoverBackgroundColor = Colors.DarkGray;
            view.TitleBar.ButtonHoverForegroundColor = Colors.White;

            //按下
            view.TitleBar.ButtonPressedBackgroundColor = Colors.DarkGray;
            view.TitleBar.ButtonPressedForegroundColor = Colors.White;

            //inactive 不是当前窗口，我觉得不常用
            view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            view.TitleBar.ButtonInactiveForegroundColor = Colors.White;
        }

    }
}
