using System.Collections.Generic;
using HtmlAgilityPack;
using Accelerator.Class;
using System.Threading.Tasks;
using System;

namespace ENG_learning.Functions
{
    class Html2Class
    {
        public Task<List<HtmlNodesClass>> DoBBCAsync(string url)
        {
            return Task.Run(() =>
            {
                List<HtmlNodesClass> htmlNodes = BBCClassWriter(BBCHtmlReader(url));
                return htmlNodes;
            });
        }

        public List<HtmlNodesClass> DoBBC(string url)
        {

            List<HtmlNodesClass> htmlNodes = BBCClassWriter(BBCHtmlReader(url));
            return htmlNodes;

        }

        private static HtmlNodeCollection BBCHtmlReader(string url)
        {
            HtmlWeb webClient = new HtmlWeb();

            HtmlDocument doc = webClient.Load(url);
            HtmlNode html_div = doc.DocumentNode.SelectSingleNode(".//div[@class='c_left2']");
            HtmlNodeCollection html_a = html_div.SelectNodes("a");
            return html_a;

        }

        private static List<HtmlNodesClass> BBCClassWriter(HtmlNodeCollection html_a)
        {
            List<HtmlNodesClass> htmlNodesList = new List<HtmlNodesClass>();
            foreach (var a in html_a)
            {
                HtmlNodesClass htmlNodes = new HtmlNodesClass
                {
                    ID = a.Attributes["href"].Value.Split('_')[2].Split('.')[0].ToString(),
                    TitleCN = a.SelectSingleNode(".//dl//dd//h2//strong").InnerText,
                    TitleEN = a.SelectSingleNode(".//dl//dd//h2//span").InnerText,
                    Brief = a.SelectSingleNode(".//dl//dd//p").InnerText,
                    NetImagePath = a.SelectSingleNode(".//dl//dt//img").Attributes["src"].Value.ToString(),
                    Date = a.SelectSingleNode(".//dl//dd//ul//li[@class='deta']").InnerText,
                    View = a.SelectSingleNode(".//dl//dd//ul//li[1]").InnerText
                };
                htmlNodesList.Add(htmlNodes);
            }
            return htmlNodesList;
        }

        public Task<List<HtmlNodesClass>> DoVOAAsync(string url)
        {
            return Task.Run(() =>
            {
                List<HtmlNodesClass> htmlNodes = VOAClassWriter(VOAHtmlReader(url));
                return htmlNodes;
            });
        }

        public List<HtmlNodesClass> DoVOA(string url)
        {

            List<HtmlNodesClass> htmlNodes = VOAClassWriter(VOAHtmlReader(url));
            return htmlNodes;
        }

        private static HtmlNodeCollection VOAHtmlReader(string url)
        {
            HtmlWeb webClient = new HtmlWeb();

            HtmlDocument doc = webClient.Load(url);
            HtmlNode html_div = doc.DocumentNode.SelectSingleNode(".//ul[@class='list-unstyled list-contents picture']");
            HtmlNodeCollection html_li = html_div.SelectNodes("li");
            return html_li;

        }

        private static List<HtmlNodesClass> VOAClassWriter(HtmlNodeCollection html_li)
        {
            List<HtmlNodesClass> htmlNodesList = new List<HtmlNodesClass>();
            foreach (var li in html_li)
            {
                HtmlNodesClass htmlNodes = new HtmlNodesClass();

                htmlNodes.ID = li.SelectSingleNode(".//a").Attributes["href"].Value.Split('_')[2].Split('.')[0].ToString();
                htmlNodes.TitleCN = li.SelectSingleNode(".//a/div[@class='caption']//span[@class='desc_cn']").InnerText.Split('】')[1];
                htmlNodes.TitleEN = li.SelectSingleNode(".//a//div[@class='caption']//span[@class='desc_en']").InnerText;
                htmlNodes.NetImagePath = li.SelectSingleNode(".//a//div[@class='image ']//img").Attributes["src"].Value.ToString();
                htmlNodes.Date = li.SelectSingleNode(".//a//div[@class='caption']//span[@class='date']").InnerText;

                htmlNodesList.Add(htmlNodes);
            }
            return htmlNodesList;
        }
    }
}