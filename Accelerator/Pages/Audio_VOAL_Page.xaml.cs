using Accelerator.Class;
using Accelerator.Functions;
using Accelerator.UserControls;
using ENG_learning.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Accelerator.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Audio_VOAL_Page : Page
    {
        private int pageIndex = 1;
        private string baseurl = "http://voa.iyuba.cn/voamansu_0_";
        ObservableCollection<HtmlNodesClass> source = new ObservableCollection<HtmlNodesClass>();
        List<HtmlNodesClass> tempList = new List<HtmlNodesClass>();
        //string ListType = "BBC6List";

        public Audio_VOAL_Page()
        {
            InitializeComponent();
            Load();

            //Class2Xml.XmlCreator(htmlNodes,ListType);
        }

        private void Audio_VOAL_Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            playallgrid.Width = grid.ActualWidth;
            
        }

        private async void Load()
        {
            try
            {
                tempList = new Html2Class().DoVOA(baseurl + pageIndex.ToString() + ".html");
                tempList.ForEach(x => source.Add(x));
                listview.ItemsSource = source;
                pageIndex++;
                //await listview.LoadMoreItemsAsync();
            }
            catch (Exception e)
            {
                Ins_Error.eMessage = e.Message;
                Ins_Error.eSource = e.Source.ToString();
                Ins_Error.eStackTrace = e.StackTrace.ToString();
                new ErrorWritter().IsErrorWritten(e.Message, e.Source.ToString(), e.StackTrace.ToString());
                Error_Control error_Control = new Error_Control();
                error_Control.Tapped += async (_s, _e) =>
                {
                    error_Control.Refreshing();
                    await Task.Delay(100);
                    Load();
                    grid.Children.Remove(error_Control);
                };
                grid.Children.Add(error_Control);
            }
        }

        private async void LoadmoreBtn_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                await LoadMoreAsync();
            });
        }

        private async Task LoadMoreAsync()
        {
            ProgressRing progressRing = new ProgressRing()
            {
                Visibility = Visibility.Visible,
                IsActive = true,
            };
            LoadmoreBtn.Content = progressRing;
            LoadmoreBtn.IsEnabled = false;

            try
            {
                progressRing.Visibility = Visibility.Collapsed;

                tempList = await new Html2Class().DoVOAAsync(baseurl + pageIndex.ToString() + ".html");
                pageIndex++;
                tempList.ForEach(x => source.Add(x));

                LoadmoreBtn.Content = "点击加载更多~";
                LoadmoreBtn.IsEnabled = true;
            }
            catch
            {
                pageIndex--;
                progressRing.Visibility = Visibility.Collapsed;
                LoadmoreBtn.IsEnabled = true;
                LoadmoreBtn.Content = "加载失败，点击重试~";
            }
        }

        private async void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = new ObjectFinder().GetParentObject<ListViewItem>((AppBarButton)sender, "");
            HtmlNodesClass content = (HtmlNodesClass)listViewItem.DataContext;

            List<LyricClass> lyricClasses = await new LyricCreator().GetVOALyric(content.ID);

            Ins_PlayItem.ins_PlayItem.ID = content.ID;
            Ins_PlayItem.ins_PlayItem.Date = content.Date;
            Ins_PlayItem.ins_PlayItem.TitleEN = content.TitleEN;
            Ins_PlayItem.ins_PlayItem.TitleCN = content.TitleCN;
            Ins_PlayItem.ins_PlayItem.NetImagePath = content.NetImagePath;
            Ins_PlayItem.ins_PlayItem.LocalImagePath = content.LocalImagePath;
            Ins_PlayItem.ins_PlayItem.Lyric = lyricClasses;
            //Ins_PlayItem.ins_PlayItem.TotalTime= Convert.ToInt32(Ins_PlayItem.ins_PlayItem.Lyric[Ins_PlayItem.ins_PlayItem.Lyric.Count() - 1].Timing);
            Ins_PlayItem.ins_PlayItem.Status = "play";

            //Ins_PlayItem.mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("http://static.iyuba.com/sounds/minutes/" + Ins_PlayItem.ins_PlayItem.ID + ".mp3"));
            //Ins_PlayItem.mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("http://118.190.169.68/sounds/voa/" + Ins_PlayItem.ins_PlayItem.Date.Split('-')[0] + Ins_PlayItem.ins_PlayItem.Date.Split('-')[1] + "/" + Ins_PlayItem.ins_PlayItem.ID + ".mp3"));
            MediaSource mediaSource = MediaSource.CreateFromUri(new Uri("http://118.190.169.68/sounds/voa/" + Ins_PlayItem.ins_PlayItem.Date.Split('-')[0] + Ins_PlayItem.ins_PlayItem.Date.Split('-')[1] + "/" + Ins_PlayItem.ins_PlayItem.ID + ".mp3"));
            Ins_PlayItem.mediaPlaybackItem =new MediaPlaybackItem(mediaSource);

            var props =Ins_PlayItem.mediaPlaybackItem.GetDisplayProperties();
            props.Type = Windows.Media.MediaPlaybackType.Music;

            props.MusicProperties.Title = content.TitleCN;
            props.MusicProperties.Artist =content.TitleEN;
            props.Thumbnail= RandomAccessStreamReference.CreateFromUri(new Uri(content.NetImagePath));
            Ins_PlayItem.mediaPlaybackItem.ApplyDisplayProperties(props);
            Ins_PlayList.mediaPlaybackList.Items.Add(Ins_PlayItem.mediaPlaybackItem);
            Ins_PlayItem.mediaPlayer.Source = Ins_PlayList.mediaPlaybackList;
            Ins_PlayItem.mediaPlayer.Play();
        }

        private void PlayAllBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            foreach (var item in source)
            {
                //http://118.190.169.68/sounds/voa/201901/7562.mp3
                string uri = "http://118.190.169.68/sounds/voa/" + item.Date.Split('-')[0]+ item.Date.Split('-')[1]+"/"+item.ID+".mp3";
                //var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(uri)));
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(uri)));
                Ins_PlayList.mediaPlaybackList.Items.Add(mediaPlaybackItem);
            }
            foreach (var item in Ins_PlayList.mediaPlaybackList.Items)
            {
                int n = 1;
            }
            new Ins_PlayList().Load();
            Ins_PlayItem.mediaTimelineController.Start();
            var currentitem = Ins_PlayList.mediaPlaybackList.CurrentItem;
            int a = 1;
        }
    }
}
