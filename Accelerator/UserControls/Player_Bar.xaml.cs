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
using Accelerator.Class;
using System.ComponentModel;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Media;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Accelerator.UserControls
{
    public sealed partial class Player_Bar : UserControl
    {

        public Player_Bar()
        {
            DataContext = Ins_PlayItem.ins_PlayItem;
            InitializeComponent();
            Ins_PlayItem.mediaPlayer.TimelineController = Ins_PlayItem.mediaTimelineController;
            Ins_PlayItem.mediaTimelineController.PositionChanged += MediaTimelineController_PositionChanged;
            Ins_PlayItem.mediaTimelineController.StateChanged += MediaTimelineController_StateChanged;
        }

        private async void MediaTimelineController_StateChanged(MediaTimelineController sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                switch (Ins_PlayItem.mediaTimelineController.State.ToString())
                {
                    case "Running":
                        Status_Btn.Tag = "pause";
                        Ins_PlayItem.ins_PlayItem.Status = "pause";
                        Status_Symbol.Symbol = Symbol.Pause;
                        break;
                    case "Paused":
                        Status_Btn.Tag = "resume";
                        Ins_PlayItem.ins_PlayItem.Status = "resume";
                        Status_Symbol.Symbol = Symbol.Play;
                        break;
                }
            });
        }

        private async void MediaTimelineController_PositionChanged(MediaTimelineController sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //slider.Value = Ins_PlayItem.mediaTimelineController.Position.Seconds;
                Ins_PlayItem.ins_PlayItem.StartTime = Ins_PlayItem.mediaTimelineController.Position.TotalSeconds;
                Ins_PlayItem.ins_PlayItem.TotalTime = Ins_PlayItem.mediaPlaybackSession.NaturalDuration.TotalSeconds;
            });
        }

        private void Status_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch (Status_Btn.Tag.ToString())
            {
                case "play":
                    Ins_PlayItem.mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("http://static.iyuba.com/sounds/minutes/" + Ins_PlayItem.ins_PlayItem.ID + ".mp3"));

                    Status_Btn.Tag = "pause";
                    Ins_PlayItem.ins_PlayItem.Status = "pause";
                    Status_Symbol.Symbol = Symbol.Pause;

                    break;

                case "pause":
                    Status_Symbol.Symbol = Symbol.Play;
                    Status_Btn.Tag = "resume";
                    Ins_PlayItem.ins_PlayItem.Status = "resume";

                    break;

                case "resume":
                    Status_Symbol.Symbol = Symbol.Pause;
                    Status_Btn.Tag = "pause";
                    Ins_PlayItem.ins_PlayItem.Status = "pause";

                    break;
            }
        }

        private void Lyric_Btn_Click(object sender, RoutedEventArgs e)
        {
            Lyric_Pane lyric_Pane = new Lyric_Pane();
            lyric_Pane.ShowAPopup();
        }
    }
}
