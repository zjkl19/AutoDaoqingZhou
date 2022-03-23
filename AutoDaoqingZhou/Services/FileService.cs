using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDaoqingZhou.Services
{
    public static class FileService
    {
        /// 根据照片编号尾数获取照片完整的名称，如果有多张相同尾数的照片，按文件名排序取第一张
        /// </summary>
        /// <param name="folderName">文件夹名称</param>
        /// <param name="pictureNo">照片编号</param>
        /// <returns></returns>
        public static string GetFileName(string folderName, string fileName, string fileExtension)
        {
            var dirs = Directory.GetFiles($@"{folderName}\", $"*{fileName}*.{fileExtension}");    //结果含有路径
            return dirs[0];
        }

    }
}
