using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Accelerator.Functions;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.ApplicationModel.Email;
using Windows.Storage.Streams;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Accelerator.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Setting_Page : Page
    {
        List<Folders> folders = new List<Folders>();

        public Setting_Page()
        {
            InitializeComponent();
            CachePathTb.Text = Setting.GetSettingValue("CachePath");
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var textBlock = ((ToggleSwitch)sender).Header;
            switch (((TextBlock)textBlock).Text)
            {
                case "自动播放":
                    switch (((ToggleSwitch)sender).IsOn)
                    {
                        case true:
                            Setting.SetSettingValue("AutoPlay", "True");
                            break;
                        case false:
                            Setting.SetSettingValue("AutoPlay", "False");
                            break;
                    }
                    break;

                case "手势拖动":
                    switch (((ToggleSwitch)sender).IsOn)
                    {
                        case true:
                            Setting.SetSettingValue("GestureSwipe", "True");
                            break;
                        case false:
                            Setting.SetSettingValue("GestureSwipe", "False");
                            break;
                    }
                    break;

                case "边听边存":
                    switch (((ToggleSwitch)sender).IsOn)
                    {
                        case true:
                            Setting.SetSettingValue("PlayNCache", "True");
                            break;
                        case false:
                            Setting.SetSettingValue("PlayNCache", "False");
                            break;
                    }
                    break;
            }
        }

        private void ToggleSwitch_Loading(FrameworkElement sender, object args)
        {
            var textBlock = ((ToggleSwitch)sender).Header;
            ToggleSwitch toggleSwitch = (ToggleSwitch)sender;

            switch (((TextBlock)textBlock).Text)
            {
                case "自动播放":
                    toggleSwitch.IsOn = StrToBool(Setting.GetSettingValue("AutoPlay"));
                    break;

                case "手势拖动":
                    toggleSwitch.IsOn = StrToBool(Setting.GetSettingValue("GestureSwipe"));
                    break;

                case "边听边存":
                    toggleSwitch.IsOn = StrToBool(Setting.GetSettingValue("PlayNCache"));
                    break;
            }
        }

        private bool StrToBool(string str)
        {
            if (str == "True")
                return true;
            return false;
        }

        private async void PathSelectHyperlinkBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker p = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };
            p.FileTypeFilter.Add("*");
            StorageFolder folder = await p.PickSingleFolderAsync();
            CachePathTb.Text = folder.Path;
            Setting.SetSettingValue("CachePath", folder.Path);
        }

        private async void OpenPathSelectHyperlinkBtn_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(Setting.GetSettingValue("CachePath"));
            await Launcher.LaunchFolderAsync(storageFolder);
        }

        private async void treeview_Loading(FrameworkElement sender, object args)
        {
            //选择存储位置
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(Setting.GetSettingValue("CachePath"));
            StorageFolder storageFolder1 = await storageFolder.GetFolderAsync("Logs");
            var storagefolders = await storageFolder1.GetFoldersAsync();
            //to 类
            foreach (StorageFolder x in storagefolders)//遍历文件夹
            {
                Folders tempfolders = new Folders();
                tempfolders.FolderName = x.DisplayName;

                var storagefiles = await x.GetFilesAsync();
                List<File> tempfiles = new List<File>();
                foreach (StorageFile y in storagefiles)//遍历文件
                {
                    File tempfile = new File();
                    tempfile.FileName = y.DisplayName;
                    tempfile.FilePath = y.Path;
                    tempfiles.Add(tempfile);
                }
                tempfolders.Files = tempfiles;
                folders.Add(tempfolders);
            }
            //to xamlcontrol
            TreeViewNode rootNode = new TreeViewNode() { Content = "🛠   Error List" };
            rootNode.IsExpanded = true;
            foreach (var folder in folders)
            {
                TreeViewNode folderNode = new TreeViewNode() { Content = "📁   " + folder.FolderName };

                foreach (var file in folder.Files)
                {
                    TreeViewNode treeViewNode = new TreeViewNode();
                    folderNode.Children.Add(new TreeViewNode() { Content = "📄   " + file.FileName });
                }
                rootNode.Children.Add(folderNode);
            }
            treeview.RootNodes.Add(rootNode);
        }

        private async void treeview_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            TreeViewNode treeViewNode = (TreeViewNode)args.InvokedItem;
            if (treeViewNode.HasChildren == false)//判断节点是否有子节点
            {
                string fileName = treeViewNode.Content.ToString().Replace("📄   ", "");

                var item = from f in folders//linq文件路径
                           from fi in f.Files
                           where fi.FileName == fileName
                           select fi.FilePath;
                StorageFile storageFile = await StorageFile.GetFileFromPathAsync(item.ElementAt(0));
                //弹窗
                ContentDialog contentDialog = new ContentDialog()
                {
                    PrimaryButtonText = "确定",
                    SecondaryButtonText = "反馈",
                };
                using (Stream file = await storageFile.OpenStreamForReadAsync())
                {
                    using (StreamReader read = new StreamReader(file))
                    {
                        contentDialog.Content= read.ReadToEnd();
                    }
                };
                contentDialog.SecondaryButtonClick +=async (_s, _e) =>
                {
                    EmailRecipient emailRecipient1 = new EmailRecipient("Snowysong@Live.com");
                    EmailMessage emailMessage = new EmailMessage();
                    emailMessage.To.Add(emailRecipient1);
                    emailMessage.Subject = "Accelerator错误反馈";
                    //添加附件
                    if (storageFile != null)
                    {
                        var stream = RandomAccessStreamReference.CreateFromFile(storageFile);
                        var attachment = new EmailAttachment(storageFile.Name, stream);
                        emailMessage.Attachments.Add(attachment);
                    }                    
                    await EmailManager.ShowComposeNewEmailAsync(emailMessage);
                };
                await contentDialog.ShowAsync();
            }
        }

        private void ReCalHBtn_Click(object sender, RoutedEventArgs e)
        {
            FilesTreeXml.XmlWritter();
        }
    }

    public class Folders
    {
        public string FolderName { get; set; }
        public List<File> Files { get; set; }
    }

    public class File
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}

