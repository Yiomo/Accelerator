using Windows.Media.Playback;

namespace Accelerator.Class
{
    class Ins_PlayList
    {
        public static MediaPlaybackList mediaPlaybackList = new MediaPlaybackList();

        public void Load()
        {
            mediaPlaybackList.MaxPlayedItemsToKeepOpen = 3;
            Ins_PlayItem.mediaPlayer.Source = mediaPlaybackList;
        }
    }
}
