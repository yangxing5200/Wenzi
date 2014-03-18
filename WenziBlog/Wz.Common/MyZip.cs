/*************************************************************************************
     *  CLR版本：       4.0.30319.18052
     * 类 名 称：       MyZip
     * 机器名称：       PLUS
     * 命名空间：       Wz.Common
     * 文 件 名：       MyZip
     * 创建时间：       2013/10/11 12:02:03
     * 作    者：       Plus.Yang(418505093@qq.com)
     * 说    明： 
     * 修改时间：
     * 修 改 人：
**************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Wz.Common
{
    public class MyZip
    {
        /// <summary>
        /// 存放待压缩的文件的绝对路径
        /// </summary>
        private static List<string> AbsolutePaths { set; get; }
        public static string ErrorMsg { set; get; }

        public MyZip()
        {
            ErrorMsg = "";
            AbsolutePaths = new List<string>();
        }
        /// <summary>
        /// 添加压缩文件
        /// </summary>
        /// <param name="fileAbsolutePath">文件的绝对路径</param>
        public static void AddFile(string fileAbsolutePath)
        {
            AbsolutePaths.Add(fileAbsolutePath);
        }
        /// <summary>
        /// 压缩文件或者文件夹
        /// </summary>
        /// <param name="depositPath">压缩后文件的存放路径   如C:\\windows\abc.zip</param>
        /// <returns></returns>
        public static bool CompressionZip(string depositPath)
        {
            bool result = true;
            FileStream fs = null;
            try
            {
                var comStream = new ZipOutputStream(File.Create(depositPath));
                comStream.SetLevel(9);      //压缩等级
                foreach (string path in AbsolutePaths)
                {
                    //如果是目录
                    if (Directory.Exists(path))
                    {
                        ZipFloder(path, comStream, path);
                    }
                    else if (File.Exists(path))//如果是文件
                    {
                        fs = File.OpenRead(path);
                        var bts = new byte[fs.Length];
                        fs.Read(bts, 0, bts.Length);
                        var ze = new ZipEntry(new FileInfo(path).Name);
                        comStream.PutNextEntry(ze);             //为压缩文件流提供一个容器
                        comStream.Write(bts, 0, bts.Length);  //写入字节
                    }
                }
                comStream.Finish(); // 结束压缩
                comStream.Close();
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                ErrorMsg = ex.Message;
                result = false;
            }
            return result;
        }
        //压缩文件夹
        private static void ZipFloder(string ofloderPath, ZipOutputStream zos, string floderPath)
        {
            foreach (FileSystemInfo item in new DirectoryInfo(floderPath).GetFileSystemInfos())
            {
                if (Directory.Exists(item.FullName))
                {
                    ZipFloder(ofloderPath, zos, item.FullName);
                }
                else if (File.Exists(item.FullName))//如果是文件
                {
                    var oDir = new DirectoryInfo(ofloderPath);
                    string fullName2 = new FileInfo(item.FullName).FullName;
                    string path = oDir.Name + fullName2.Substring(oDir.FullName.Length, fullName2.Length - oDir.FullName.Length);//获取相对目录
                    FileStream fs = File.OpenRead(fullName2);
                    var bts = new byte[fs.Length];
                    fs.Read(bts, 0, bts.Length);
                    var ze = new ZipEntry(path);
                    zos.PutNextEntry(ze);             //为压缩文件流提供一个容器
                    zos.Write(bts, 0, bts.Length);  //写入字节
                }
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="depositPath">压缩文件路径</param>
        /// <returns></returns>
        public static bool DeCompressionZip(string depositPath)
        {
            return DeCompressionZip(depositPath, null);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="depositPath">压缩文件路径</param>
        /// <param name="_floderPath">解压的路径</param>
        /// <returns></returns>
        public static bool DeCompressionZip(string depositPath, string _floderPath)
        {
            if (_floderPath == null) throw new ArgumentNullException("_floderPath");
            bool result = true;
            FileStream fs = null;
            try
            {
                var inpStream = new ZipInputStream(File.OpenRead(depositPath));
                ZipEntry ze = inpStream.GetNextEntry();//获取压缩文件中的每一个文件
                Directory.CreateDirectory(_floderPath);//创建解压文件夹
                while (ze != null)//如果解压完ze则是null
                {
                    if (ze.IsFile)//压缩zipINputStream里面存的都是文件。带文件夹的文件名字是文件夹\\文件名
                    {
                        string[] strs = ze.Name.Split('\\');//如果文件名中包含’\\‘则表明有文件夹
                        if (strs.Length > 1)
                        {
                            //两层循环用于一层一层创建文件夹
                            for (int i = 0; i < strs.Length - 1; i++)
                            {
                                string floderPath = _floderPath;
                                for (int j = 0; j < i; j++)
                                {
                                    floderPath = floderPath + "\\" + strs[j];
                                }
                                floderPath = floderPath + "\\" + strs[i];
                                Directory.CreateDirectory(floderPath);
                            }
                        }
                        fs = new FileStream(_floderPath + "\\" + ze.Name, FileMode.OpenOrCreate, FileAccess.Write);//创建文件
                        //循环读取文件到文件流中
                        while (true)
                        {
                            var bts = new byte[1024];
                            int i = inpStream.Read(bts, 0, bts.Length);
                            if (i > 0)
                            {
                                fs.Write(bts, 0, i);
                            }
                            else
                            {
                                fs.Flush();
                                fs.Close();
                                break;
                            }
                        }
                    }
                    ze = inpStream.GetNextEntry();
                }
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                ErrorMsg = ex.Message;
                result = false;
            }
            return result;
        }

    }
}
