using System;
using Newtonsoft.Json;
using Qiniu.Share.IO;
using Qiniu.Share.Util;

namespace Qiniu.Share.Storage
{
    /// <summary>
    /// 断点续上传辅助函数Load/Save
    /// </summary>
    public class ResumeHelper
    {

        /// <summary>
        /// 生成默认的断点记录文件名称
        /// </summary>
        /// <param name="localFile">待上传的本地文件</param>
        /// <param name="key">要保存的目标key</param>
        /// <returns>用于记录断点信息的文件名</returns>
        public static string GetDefaultRecordKey(string localFile, string key)
        {
            string tempDir = IOUtils.Api.GetTempPath();
            var fileInfo = IOUtils.Api.GetFileInfo(localFile);
            string uniqueKey = string.Format("{0}:{1}:{2}", localFile, key, fileInfo.UpdatedAt.ToFileTime());
            return string.Format("{0}\\{1}",tempDir,"QiniuResume_" + Hashing.CalcMD5X(uniqueKey));
        }

        /// <summary>
        /// 尝试从从文件载入断点信息
        /// </summary>
        /// <param name="recordFile">断点记录文件</param>
        /// <returns>断点信息</returns>
        public static ResumeInfo Load(string recordFile)
        {
            ResumeInfo resumeInfo = null;

            try
            {
                resumeInfo = JsonConvert.DeserializeObject<ResumeInfo>(IOUtils.Api.ReadFile(recordFile));
            }
            catch (Exception)
            {
                resumeInfo = null;
            }

            return resumeInfo;
        }

        /// <summary>
        /// 保存断点信息到文件
        /// </summary>
        /// <param name="resumeInfo">断点信息</param>
        /// <param name="recordFile">断点记录文件</param>
        public static void Save(ResumeInfo resumeInfo, string recordFile)
        {
            var jsonStr = resumeInfo.ToJsonStr();
            IOUtils.Api.WriteFile(recordFile,jsonStr);
        }
    }
}
