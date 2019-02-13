using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.Class
{
    public class LyricClass
    {
        public string ImgPath { get; set; }
        public string ParaId { get; set; }
        public string IdIndex { get; set; }
        public string Announcer { get; set; }
        public string Sentence_cn { get; set; }
        public string ImgWords { get; set; }
        public string Timing { get; set; }
        public string Sentence { get; set; }
        public string BbcId { get; set; }

        public string total { get; set; }
        public List<LyricJsonClass.Data> data { get; set; }
    }
}
