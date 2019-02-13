using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Accelerator.Functions
{
    class Setting
    {
        public static bool IsFirstlyRun()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values["RunTimes"] == null)
                return true;
            else
                return false;
        }

        public static async void InitializeSettings()
        {

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            StorageFolder storageFolder = await KnownFolders.MusicLibrary.CreateFolderAsync("Accelerator", CreationCollisionOption.OpenIfExists);

            localSettings.Values["RunTimes"] = "1";
            localSettings.Values["AutoPlay"] = "True";
            localSettings.Values["GestureSwipe"] = "True";
            localSettings.Values["CachePath"] = storageFolder.Path;
        }

        public static bool SetSettingValue(string key, string value)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[key] = value;
            return true;
        }

        public static string GetSettingValue(string key)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string result = localSettings.Values[key] as string;
            return result;
        }
    }
}
