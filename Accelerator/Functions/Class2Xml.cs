using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using Accelerator.Class;
using Windows.Storage;

namespace Accelerator.Functions
{
    class Class2Xml
    {
        static XmlDocument doc = new XmlDocument();
        static string path;

        public static async void XmlCreator(List<HtmlNodesClass> htmlNodesList, string listType)
        {
            StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Xml", CreationCollisionOption.OpenIfExists);
            path = folder.Path + "//" + listType + ".xml";
            try
            {
                StorageFile file = await folder.CreateFileAsync(listType + ".xml", CreationCollisionOption.FailIfExists);
                await FileIO.WriteTextAsync(file, "<?xml version=\"1.0\" encoding=\"utf-8\" ?><List><Index></Index></List>");
            }
            catch
            { }
            finally
            {
                doc.Load(path);
                //doc.LoadXml(path);
            }

            //XmlElement root = doc.DocumentElement;
            XmlElement root = (XmlElement)doc.SelectSingleNode("//List");
            XmlElement index = (XmlElement)doc.SelectSingleNode("//Index");

            //List<string> Ids = new List<string>();
            //foreach (XmlElement y in doc.SelectNodes(".//List//Item//ID"))
            //{
            //    Ids.Add(y.InnerText);
            // }

            foreach (var x in htmlNodesList)
            {
                if (index.InnerText.Contains(x.ID) == false)
                {
                    index.InnerText += x.ID + ",";

                    XmlElement item = doc.CreateElement("Item");
                    XmlElement id = doc.CreateElement("ID");
                    XmlElement date = doc.CreateElement("Date");
                    XmlElement localImagePath = doc.CreateElement("LocalImagePath");
                    XmlElement netImagePath = doc.CreateElement("NetImagePath");
                    XmlElement titleCN = doc.CreateElement("TitleCN");
                    XmlElement titleEN = doc.CreateElement("TitleEN");
                    XmlElement brief = doc.CreateElement("Brief");
                    XmlElement view = doc.CreateElement("View");

                    id.InnerText = x.ID;
                    date.InnerText = x.Date;
                    localImagePath.InnerText = "http://localpath.path";
                    netImagePath.InnerText = x.NetImagePath;
                    titleCN.InnerText = x.TitleCN;
                    titleEN.InnerText = x.TitleEN;
                    brief.InnerText = x.Brief;
                    view.InnerText = x.View;

                    item.AppendChild(id);
                    item.AppendChild(date);
                    item.AppendChild(netImagePath);
                    item.AppendChild(localImagePath);
                    item.AppendChild(titleCN);
                    item.AppendChild(titleEN);
                    item.AppendChild(brief);
                    item.AppendChild(view);

                    root.AppendChild(item);
                    doc.Load(path);
                }
            }
        }
    }
}
