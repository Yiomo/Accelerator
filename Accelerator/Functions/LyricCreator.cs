using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using ENG_learning.Functions;
using Newtonsoft.Json;
using Accelerator.Class;
using System.IO;

namespace Accelerator.Functions
{
    class LyricCreator
    {
        public Task<List<LyricClass>> GetBBCLyric(string id)
        {
            return Task.Run(async () =>
            {
                HttpResponseMessage response = await new SimRequest().SimPost("http://apps.iyuba.com/minutes/textApi.jsp?bbcid=" + id + "&format=json");
                List<LyricClass> lyric = new List<LyricClass>();

                string jsonstr = response.Content.ReadAsStringAsync().Result;
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(jsonstr);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);

                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);

                LyricJsonClass.RootObject rb1 = JsonConvert.DeserializeObject<LyricJsonClass.RootObject>(textWriter.ToString());

                List<LyricCreator> lyrics = new List<LyricCreator>();
                List<string> ParaId = new List<string>();
                List<string> IdIndex = new List<string>();
                List<string> Announcer = new List<string>();
                List<string> Sentence = new List<string>();
                List<string> Sentence_cn = new List<string>();
                List<string> BbcId = new List<string>();
                List<string> Timing = new List<string>();

                foreach (LyricJsonClass.Data data1 in rb1.data)
                {
                    ParaId.Add(data1.ParaId);
                    IdIndex.Add(data1.IdIndex);
                    Announcer.Add(data1.Announcer);
                    Sentence.Add(data1.Sentence);
                    Sentence_cn.Add(data1.sentence_cn);
                    BbcId.Add(data1.BbcId);
                    Timing.Add(data1.Timing);
                }

                for (int i = 0; i < Sentence.Count; i++)
                {
                    LyricClass Lyrics = new LyricClass()
                    {
                        ParaId = ParaId[i],
                        IdIndex = IdIndex[i],
                        Announcer = Announcer[i],
                        Sentence = Sentence[i],
                        Sentence_cn = Sentence_cn[i],
                        BbcId = BbcId[i],
                        Timing = Timing[i]
                    };
                    lyric.Add(Lyrics);
                }

                return lyric;
            });
        }

        public Task<List<LyricClass>> GetVOALyric(string id)
        {
            return Task.Run(async () =>
            {
                HttpResponseMessage response = await new SimRequest().SimPost("http://apps.iyuba.cn/voa/textApi.jsp?voaid=" + id + "&format=json");
                List<LyricClass> lyric = new List<LyricClass>();

                string jsonstr = response.Content.ReadAsStringAsync().Result;
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(jsonstr);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);

                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);

                LyricJsonClass.RootObject rb1 = JsonConvert.DeserializeObject<LyricJsonClass.RootObject>(textWriter.ToString());

                List<LyricCreator> lyrics = new List<LyricCreator>();
                List<string> ParaId = new List<string>();
                List<string> IdIndex = new List<string>();
                List<string> Announcer = new List<string>();
                List<string> Sentence = new List<string>();
                List<string> Sentence_cn = new List<string>();
                List<string> BbcId = new List<string>();
                List<string> Timing = new List<string>();

                foreach (LyricJsonClass.Data data1 in rb1.data)
                {
                    ParaId.Add(data1.ParaId);
                    IdIndex.Add(data1.IdIndex);
                    Announcer.Add(data1.Announcer);
                    Sentence.Add(data1.Sentence);
                    Sentence_cn.Add(data1.sentence_cn);
                    BbcId.Add(data1.BbcId);
                    Timing.Add(data1.Timing);
                }

                for (int i = 0; i < Sentence.Count; i++)
                {
                    LyricClass Lyrics = new LyricClass()
                    {
                        ParaId = ParaId[i],
                        IdIndex = IdIndex[i],
                        Announcer = Announcer[i],
                        Sentence = Sentence[i],
                        Sentence_cn = Sentence_cn[i],
                        BbcId = BbcId[i],
                        Timing = Timing[i]
                    };
                    lyric.Add(Lyrics);
                }

                return lyric;
            });
        }

    }
}
