using System.Collections.Generic;

namespace Accelerator.Class
{
    public class LyricJsonClass
    {
        public string ParaId { get; internal set; }

        public class Data
        {
            public string ImgPath { get; set; }
            public string ParaId { get; set; }
            public string IdIndex { get; set; }
            public string Announcer { get; set; }
            public string sentence_cn { get; set; }
            public string ImgWords { get; set; }
            public string Timing { get; set; }
            public string Sentence { get; set; }
            public string BbcId { get; set; }
        }

        public class RootObject
        {
            public string total { get; set; }
            public List<Data> data { get; set; }
        }
    }
}
