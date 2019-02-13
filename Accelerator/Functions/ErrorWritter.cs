using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Profile;

namespace Accelerator.Functions
{
    class ErrorWritter
    {
        public async void IsErrorWritten(string eMessage,string eSource,string eStackTrace)
        {
            StorageFolder folder = null;
            StorageFolder folder1 = null;
            StorageFolder folder2 = null;
            StorageFile newFile = null;
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;

            folder=await KnownFolders.MusicLibrary.CreateFolderAsync("Accelerator", CreationCollisionOption.OpenIfExists);
            folder1 = await folder.CreateFolderAsync("Logs",CreationCollisionOption.OpenIfExists);
            folder2= await folder1.CreateFolderAsync(currentTime.Year.ToString() +"-"+currentTime.Month.ToString() + "-"+ currentTime.Day.ToString(), CreationCollisionOption.OpenIfExists);
            newFile = await folder2.CreateFileAsync(currentTime.Hour.ToString()+"-"+currentTime.Minute.ToString() + "-" + currentTime.Second+".txt", CreationCollisionOption.OpenIfExists);
            
            StorageStreamTransaction storageStreamTransaction =await newFile.OpenTransactedWriteAsync();
            DataWriter dataWriter = new DataWriter(storageStreamTransaction.Stream);

            string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong v = ulong.Parse(sv);
            ulong v1 = (v & 0xFFFF000000000000L) >> 48;
            ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            ulong v4 = (v & 0x000000000000FFFFL);
            string version = $"{v1}.{v2}.{v3}.{v4}";

            dataWriter.WriteString("<----------ErrorTime---------->" + "\r\n" + DateTime.Now.ToString() + "\r\n" +
                "<----------ErrorMessage---------->" + "\r\n" + eMessage + "\r\n" +
                "<----------ErrorSource---------->" + "\r\n" + eSource + "\r\n" +
                "<----------ErrorStackTrace---------->" + "\r\n" + eStackTrace + "\r\n" +
                "<----------Environment---------->" + "\r\n"+version);
            storageStreamTransaction.Stream.Size = await dataWriter.StoreAsync();
            await storageStreamTransaction.CommitAsync();
            dataWriter.Dispose();
            storageStreamTransaction.Dispose();
        }
    }
}
