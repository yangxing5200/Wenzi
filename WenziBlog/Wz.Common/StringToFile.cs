using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wz.Common
{
    public class StringToFile
    {
        /// <summary>
        /// 将string数据保存为一个xml文件到指定的目录里
        /// </summary>
        /// <param name="RootPath">指定的目录</param>
        /// <param name="text">要保存的文本</param>
        /// <returns>文件的路径及文件名</returns>
        public static string StringToXmlFile(string Path, string fileName, string text)
        {
            //生成文件名
            //string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            fileName = Path + fileName + ".xml";

            if (!Directory.Exists(Path)) 
            {
                Directory.CreateDirectory(Path);
            }

            File.WriteAllText(fileName, text, Encoding.UTF8);
            return fileName;
        }
    }
}
