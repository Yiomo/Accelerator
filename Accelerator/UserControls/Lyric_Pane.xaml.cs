using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Accelerator.Class;
using Accelerator.Functions;
using Windows.Media.Core;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Accelerator.UserControls
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Lyric_Pane : Page
    {
        private Popup _popup = null;
        private double _scrollviewActualHeight;
        private double _scrollviewExtendHeight;
        private double _scrollviewScrollbleHeight;
        private double _scrollviewVerticalOffset;
        private double percent;
        private ScrollViewer scrollViewer;

        private List<LyricClass> tempLyric = new List<LyricClass>();

        public Lyric_Pane()
        {
            DataContext = Ins_PlayItem.ins_PlayItem;
            this.InitializeComponent();
            _popup = new Popup();
            ApplicationView.GetForCurrentView().VisibleBoundsChanged += (s, e) =>
            {
                MeasurePopupSize();
            };
            MeasurePopupSize();
            _popup.Child = this;

            this.Loaded += PopupNoticeLoaded;
        }

        public void ShowAPopup()
        {
            _popup.IsOpen = true;
        }

        public void PopupNoticeLoaded(object sender, RoutedEventArgs e)
        {
            LyricListView.ItemsSource = Ins_PlayItem.ins_PlayItem.Lyric;
            scrollViewer = new ObjectFinder().GetChildObject<ScrollViewer>(LyricListView, "");
            tempLyric = Ins_PlayItem.ins_PlayItem.Lyric;
        }

        private async void LyricListView_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer = new ObjectFinder().GetChildObject<ScrollViewer>(LyricListView, "");
            await Task.Delay(1);

            slider.ValueChanged += Slider_ValueChanged;
            //scrollViewer.ChangeView(null, scrollViewer.ScrollableHeight, null, true);

            if (scrollViewer != null)
            {
                _scrollviewActualHeight = scrollViewer.ActualHeight;
                _scrollviewExtendHeight = scrollViewer.ExtentHeight;
                _scrollviewScrollbleHeight = scrollViewer.ScrollableHeight;
                _scrollviewVerticalOffset = scrollViewer.VerticalOffset;
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            foreach (var item in tempLyric)
            {
                if (item.Timing == Convert.ToInt32(slider.Value).ToString())
                {
                    percent = Math.Round(slider.Value / slider.Maximum, 2);
                    scrollViewer.ChangeView(null, _scrollviewScrollbleHeight * percent, null);
                }
            }
        }

        private void ScrollToElement(ScrollViewer scrollViewer, UIElement uiElement)
        {
            var transform = uiElement.TransformToVisual(scrollViewer);
            var point = transform.TransformPoint(new Point(0, 0));
            if (point.Y != 0)
            {
                var y = point.Y + scrollViewer.VerticalOffset;
                scrollViewer.ChangeView(null, y, null, true);
            }
        }

        private void MeasurePopupSize()
        {
            this.Width = ApplicationView.GetForCurrentView().VisibleBounds.Width;
            this.Height = ApplicationView.GetForCurrentView().VisibleBounds.Height;

            if (scrollViewer != null)
            {
                _scrollviewActualHeight = scrollViewer.ActualHeight;
                _scrollviewExtendHeight = scrollViewer.ExtentHeight;
                _scrollviewScrollbleHeight = scrollViewer.ScrollableHeight;
                _scrollviewVerticalOffset = scrollViewer.VerticalOffset;
            }
        }

        private void ExitFullScreen_Btn_Click(object sender, RoutedEventArgs e)
        {
            _popup.IsOpen = false;
        }

        private void Status_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch (Status_Btn.Tag.ToString())
            {
                case "play":

                    Status_Btn.Tag = "pause";
                    Ins_PlayItem.ins_PlayItem.Status = "pause";
                    Status_Symbol.Symbol = Symbol.Pause;
                    //Ins_PlayItem.mediaTimelineController.Start();
                    Ins_PlayItem.mediaPlayer.TimelineController.Start();
                    break;

                case "pause":
                    Status_Symbol.Symbol = Symbol.Play;
                    Status_Btn.Tag = "resume";
                    Ins_PlayItem.ins_PlayItem.Status = "resume";
                    //Ins_PlayItem.mediaTimelineController.Pause();
                    Ins_PlayItem.mediaPlayer.TimelineController.Pause();
                    break;

                case "resume":
                    Status_Symbol.Symbol = Symbol.Pause;
                    Status_Btn.Tag = "pause";
                    Ins_PlayItem.ins_PlayItem.Status = "pause";
                    Ins_PlayItem.mediaTimelineController.Resume();
                    //Ins_PlayItem.mediaPlayer.Play();
                    break;
            }
        }

    }
}
