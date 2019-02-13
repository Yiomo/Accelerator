using Windows.Media;
using Windows.Media.Playback;

namespace Accelerator.Class
{
    class Ins_PlayItem
    {
        public static PlayItemClass ins_PlayItem = new PlayItemClass();
        public static MediaPlayer mediaPlayer = new MediaPlayer();
        public static MediaPlaybackSession mediaPlaybackSession = mediaPlayer.PlaybackSession;
        public static MediaTimelineController mediaTimelineController = new MediaTimelineController();
        public static MediaPlaybackItem mediaPlaybackItem;
    }
}
