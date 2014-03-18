using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using Common.Logging;

namespace Wz.Common
{
    public class CommonFuncs
    {
        private static readonly ILog _log = LogManager.GetLogger("bllLog");
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static byte[] CompressionImage(Stream fileStream, long quality)
        {
            try
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(fileStream))
                {
                    using (Bitmap bitmap = new Bitmap(img))
                    {
                        ImageCodecInfo CodecInfo = GetEncoder(img.RawFormat);
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, CodecInfo, myEncoderParameters);
                            myEncoderParameters.Dispose();
                            myEncoderParameter.Dispose();
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fileStream.Close();
            }
        }



        public static Stream getMap(string url)
        {
            WebRequest request = null;
            WebResponse response = null;
            try
            {
                url = url.Replace("\\", "/");
                request = WebRequest.Create(url);
                response = request.GetResponse();
                Stream str = response.GetResponseStream();

                return str;

            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static byte[] GetByte(string url)
        {
            try
            {
                Stream stream = null;
                if (url.StartsWith("http") || url.StartsWith("ftp"))
                {
                    stream = getMap(url);
                    var ms = (MemoryStream)stream;
                    if (ms != null) return ms.ToArray();
                }
                else
                {
                    stream = getMap(url);
                    var b = new byte[stream.Length];
                    stream.Read(b, 0, b.Length);
                    stream.Seek(0, SeekOrigin.Begin);
                    return b;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new byte[] { };
        }


        public static string GenerateCode(object obj)
        {
            if (obj == null) return string.Empty;
            string code = DateTime.Now.ToString("yyyyMMddHHmmss");
            string end = obj.GetHashCode().ToString();
            if (end.Length > 6) end = end.Substring(end.Length - 6);
            else end = end.PadLeft(6, '0');

            return code + end;
        }

        /// <summary>
        /// 将文本追加到文件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="FilePath"></param>
        public static void WriteTextToFile(string text, string FilePath)
        {
            try
            {
                _log.Info("Begin to write html file...");
                _log.Info(FilePath);
                FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(text);
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
                _log.Info("End to write html file...");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
            }
        }

        /// <summary>
        /// Base64 解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64ToString(string str)
        {
            return System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(str));
        }


    }
}
