using System.IO;

namespace Qiniu.Share.IO
{
    public class CommonIOApi : IIOUtils
    {

        public bool FileExist(string localFilePath)
        {
            return File.Exists(localFilePath);
        }

        public void FileDelete(string localFilePath)
        {
            File.Delete(localFilePath);
        }

        public FileInfo GetFileInfo(string localFilePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(localFilePath);
            return new FileInfo
            {
                Name = fileInfo.Name,
                UpdatedAt = fileInfo.LastWriteTime,
                Size = fileInfo.Length
            };
        }

        public string GetTempPath()
        {
            return System.Environment.GetEnvironmentVariable("TEMP");
        }

        public Stream OpenStreamForRead(string localFilePath)
        {
            return new FileStream(localFilePath,FileMode.Open);
        }

        public Stream OpenStreamForWrite(string localFilePath)
        {
            return new FileStream(localFilePath, FileMode.OpenOrCreate);
        }

        public string ReadFile(string localFilePath)
        {
            using (var fs = OpenStreamForRead(localFilePath))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public void WriteFile(string localFilePath,string content)
        {
            using (var fs = OpenStreamForWrite(localFilePath))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                }
            }
        }
    }
}