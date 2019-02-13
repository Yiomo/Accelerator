using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Windows.Storage;
using Accelerator.Class;

namespace ENG_learning.Functions
{
    class Xml2Class
    {
        public List<HtmlNodesClass> XmlReader(string ListType)
        {
            XDocument xdoc = XDocument.Load(ApplicationData.Current.LocalFolder.Path + "\\Xml" + "\\" + ListType + ".xml");
            XElement xElement = XElement.Parse(xdoc.Document.ToString());
            List<HtmlNodesClass> htmlNodesList = new List<HtmlNodesClass>();

            var root = from x in xElement.DescendantsAndSelf("Item") select x;
            foreach (var item in root)
            {
                HtmlNodesClass htmlNodes = new HtmlNodesClass
                {
                    ID = item.Element("ID").Value.Trim(),
                    Date = item.Element("Date").Value.Trim(),
                    LocalImagePath = item.Element("LocalImagePath").Value.Trim(),
                    NetImagePath = item.Element("NetImagePath").Value.Trim(),
                    TitleCN = item.Element("TitleCN").Value.Trim(),
                    TitleEN = item.Element("TitleEN").Value.Trim(),
                    Brief = item.Element("Brief").Value.Trim()
                };

                htmlNodesList.Add(htmlNodes);
            }
            return htmlNodesList;
        }
    }
}