using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Qiniu.Share;
using Qiniu.Share.IO;
using FileInfo = Qiniu.Share.IO.FileInfo;

namespace Qiniu.Uwp.FileStorage
{
    public class UwpFileApi : IIOUtils
    {
        public bool FileExist(string localFilePath)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var directoryName = Path.GetDirectoryName(localFilePath);
                var currentFolder = await StorageFolder.GetFolderFromPathAsync(directoryName);
                return currentFolder.GetItemAsync(Path.GetFileName(localFilePath)) != null;
            }).Unwrap();

            return task.Result;
        }

        public void FileDelete(string localFilePath)
        {
            var currentFile = StorageFile.GetFileFromPathAsync(localFilePath).GetResults();
            currentFile.DeleteAsync().GetResults();

            var task = Task.Factory.StartNew(async () =>
            {
                var currentStorageFile = await StorageFile.GetFileFromPathAsync(localFilePath);
                await currentStorageFile.DeleteAsync();
            });

            task.Wait();
        }

        public FileInfo GetFileInfo(string localFilePath)
        {
            var task = Task.Factory.StartNew(async () =>
            { 
                var currentStorageFile = await StorageFile.GetFileFromPathAsync(localFilePath);
                var fileProperty = await currentStorageFile.GetBasicPropertiesAsync();
                return new FileInfo
                {
                    Name = currentStorageFile.Name,
                    Size = (long) fileProperty.Size,
                    UpdatedAt = fileProperty.DateModified.DateTime
                };
            });

            return task.Result.Result;
        }

        public string GetTempPath()
        {
            throw new NotImplementedException();
        }

        public Stream OpenStreamForRead(string localFilePath)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var currentStorageFile = await StorageFile.GetFileFromPathAsync(localFilePath);
                return await currentStorageFile.OpenStreamForReadAsync();
            });

            return task.Result.Result;
        }

        public Stream OpenStreamForWrite(string localFilePath)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var currentStorageFile = await StorageFile.GetFileFromPathAsync(localFilePath);
                return await currentStorageFile.OpenStreamForWriteAsync();
            });

            return task.Result.Result;
        }

        public void CreateFile(string filePath)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var directory = Path.GetDirectoryName(filePath);
                var folder = await StorageFolder.GetFolderFromPathAsync(directory);
                await folder.CreateFileAsync(Path.GetFileName(filePath), CreationCollisionOption.ReplaceExisting);
            });

            task.Wait();
        }

        public string ReadFile(string localFilePath)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var currentStorageFile = await StorageFile.GetFileFromPathAsync(localFilePath);
                return await FileIO.ReadTextAsync(currentStorageFile);
            });

            return task.Result.Result;
        }

        public void WriteFile(string localFilePath, string content)
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var currentStorageFile = await StorageFile.GetFileFromPathAsync(localFilePath);
                await FileIO.WriteTextAsync(currentStorageFile, content);
            });

            task.Wait();
        }
    }
}
