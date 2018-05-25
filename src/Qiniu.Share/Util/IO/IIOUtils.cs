using System.IO;

namespace Qiniu.Share.IO
{
    public interface IIOUtils
    {
        bool FileExist(string localFilePath);

        void FileDelete(string localFilePath);

        FileInfo GetFileInfo(string localFilePath);

        string GetTempPath();

        Stream OpenStreamForRead(string localFilePath);

        Stream OpenStreamForWrite(string localFilePath);

        void CreateFile(string filePath);

        string ReadFile(string localFilePath);

        void WriteFile(string localFilePath, string content);
    }
}