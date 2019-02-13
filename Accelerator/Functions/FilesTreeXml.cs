using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using Accelerator.Class;
using Windows.Storage;

namespace Accelerator.Functions
{
    class FilesTreeXml
    {
        public static XmlDocument document = new XmlDocument();
        public async static void XmlWritter()
        {
            string folderPath = Setting.GetSettingValue("CachePath");
            StorageFile TreeXmlfiles =await ApplicationData.Current.LocalFolder.CreateFileAsync("FileTree.xml",CreationCollisionOption.OpenIfExists);

            await FileIO.WriteTextAsync(TreeXmlfiles, "<?xml version=\"1.0\" encoding=\"utf-8\" ?><FileTree><List></List></FileTree>");
            document.Load(TreeXmlfiles.Path);

            XmlElement root = (XmlElement)document.SelectSingleNode(".//List");

            StorageFolder storageFolder =await StorageFolder.GetFolderFromPathAsync(folderPath);
            var size = (await storageFolder.GetBasicPropertiesAsync()).Size;
            FilesTraversal(storageFolder,root);
            //var rootfiles = await storageFolder.GetFilesAsync();
        }

        private static void FolderTraversal()
        {

        }
        private static async void FilesTraversal(StorageFolder rootfolder,XmlElement parentXmlElement)
        {
            //rootfolder=Accelerator
            //doc=FileTree.xml
            //rootXmlElement=<List></List>
            var items = await rootfolder.GetItemsAsync();
            XmlElement folderXmlElement = document.CreateElement(rootfolder.Name);

            foreach (var item in items)
            {
                if (item is StorageFile)
                {
                    XmlElement fileXmlElement = document.CreateElement("File");
                    fileXmlElement.SetAttribute("Name",item.Name);
                    fileXmlElement.SetAttribute("Size", (await item.GetBasicPropertiesAsync()).Size.ToString ());

                    folderXmlElement.AppendChild(fileXmlElement);
                    parentXmlElement.AppendChild(folderXmlElement);
                } else if (item is StorageFolder)
                {
                    XmlElement pXmlElement = document.CreateElement(item.Name);
                    FilesTraversal((StorageFolder)item,pXmlElement);
                }
            }
        }
    }
}
