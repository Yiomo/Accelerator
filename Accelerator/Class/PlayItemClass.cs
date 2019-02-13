using System.Collections.Generic;
using System.ComponentModel;

namespace Accelerator.Class
{
    public class PlayItemClass : BasicClass, INotifyPropertyChanged
    {
        private string _id = "0000";
        private string _netImagePath = "netimagepath";
        private string _localImagePath = "localimagpath";
        private List<LyricClass> _lyric;
        private string _titleEN = "this is a English title";
        private string _titleCN = "中文标题";
        private double _totalTime = 300;
        private double _startTime = 0;
        private string _status = "play";

        public new string ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                }
            }
        }

        public new string NetImagePath
        {
            get { return _netImagePath; }
            set
            {
                if (_netImagePath != value)
                {
                    _netImagePath = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NetImagePath"));
                }
            }
        }

        public new string LocalImagePath
        {
            get { return _localImagePath; }
            set
            {
                if (_localImagePath != value)
                {
                    _localImagePath = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("LocalImagePath"));
                }
            }
        }

        public List<LyricClass> Lyric
        {
            get { return _lyric; }
            set
            {
                if (_lyric != value)
                {
                    _lyric = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Lyric"));
                }
            }
        }

        public string TitleEN
        {
            get { return _titleEN; }
            set
            {
                if (_titleEN != value)
                {
                    _titleEN = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("TitleEN"));
                }
            }
        }

        public string TitleCN
        {
            get { return _titleCN; }
            set
            {
                if (_titleCN != value)
                {
                    _titleCN = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("TitleCN"));
                }
            }

        }

        public double TotalTime
        {
            get { return _totalTime; }
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalTime"));
                }
            }

        }

        public double StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("StartTime"));
                }
            }

        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsPlaying"));
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
