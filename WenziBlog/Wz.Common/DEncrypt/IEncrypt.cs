using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Maticsoft.Common.DEncrypt
{
    /// <summary>
    /// 对称加密接口
    /// </summary>
   public interface IEncrypt
    {

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password">需加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        string Encrypt(string password);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="password">需解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        string Decrypt(string password);
        /// <summary>
        /// 字节数组加密
        /// </summary>
        /// <param name="passowrd">字节数组</param>
        /// <returns>字节数组</returns>
        byte[] Encrypt(ref byte[] passowrd);
        /// <summary>
        /// 字节数组解密
        /// </summary>
        /// <param name="password">字节数组</param>
        /// <returns>字节数组</returns>
        byte[] Decrypt(ref byte[] password);
    }


    /// <summary>
    /// 枚举
    /// </summary>
    public enum EncryptType { Rijndael = 0, SHA1 = 1, DES = 2, };


    /// <summary>
    /// 对称加密类
    /// </summary>
    public class RijndaelEncrypt : IEncrypt
    {
        //加密常量
        private byte[] Key = { 23, 81, 10, 23, 19, 46, 64, 32, 92, 144, 164, 01, 189, 76, 32, 16, 198, 110, 23, 27, 22, 163, 11, 28, 133, 122, 108, 24, 78, 67, 89, 16 };
        //加密密钥
        private byte[] IV = { 198, 110, 23, 27, 22, 163, 11, 28, 133, 122, 108, 24, 78, 67, 89, 16 };
        //加密类成员
        private RijndaelManaged _rijndael;

        public RijndaelEncrypt()
        {
            _rijndael = new RijndaelManaged();
        }

        #region IEncrypt 字符串成员


        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>加密字符串</returns>
        public string Encrypt(string password)
        {
            byte[] input = UTF8Encoding.UTF8.GetBytes(password);

            byte[] output = this.Encrypt(ref input);

            //return UnicodeEncoding.UTF8.GetString(output);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="password">需解密的字符串</param>
        /// <returns>返回明文</returns>
        public string Decrypt(string password)
        {
            //byte[] input = UTF8Encoding.UTF8.GetBytes(password);
            byte[] input = Convert.FromBase64String(password);

            byte[] output = this.Decrypt(ref input);

            return UnicodeEncoding.UTF8.GetString(output);
        }

        #endregion

        #region IEncrypt 字节成员

        /// <summary>
        /// 字节数组加密
        /// </summary>
        /// <param name="passowrd">输入字节数组</param>
        /// <returns>返回加密后的数组</returns>
        public byte[] Encrypt(ref byte[] passowrd)
        {
            MemoryStream memStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(memStream, _rijndael.CreateEncryptor(Key, IV), CryptoStreamMode.Write);

            try
            {
                cStream.Write(passowrd, 0, passowrd.Length);

                cStream.FlushFinalBlock();

                return memStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("加密发生错误:" + ex.Message);
            }
            finally
            {
                cStream.Close();
                memStream.Close();
            }
        }

        /// <summary>
        /// 字节数组解密
        /// </summary>
        /// <param name="password">输入已加密的字节数组</param>
        /// <returns>返回解密后的数组</returns>
        public byte[] Decrypt(ref byte[] password)
        {
            MemoryStream memStream = new MemoryStream(password, 0, password.Length);
            CryptoStream cStream = new CryptoStream(memStream, _rijndael.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cStream);

            try
            {
                byte[] result = UnicodeEncoding.UTF8.GetBytes(sr.ReadToEnd());

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("解密发生错误:" + ex.Message);
            }
            finally
            {
                sr.Close();
                cStream.Close();
                memStream.Close();
            }
        }
        #endregion
    }

    //// <summary>
    /// 对应加解密类　采用策略模式
    /// </summary>
    public class PVEncrypt : IEncrypt
    {
        private IEncrypt _encrypt;
        private RijndaelEncrypt _rijndael;

        public PVEncrypt()
        {
            _rijndael = new RijndaelEncrypt();
            _encrypt = (IEncrypt)_rijndael;
        }

        public PVEncrypt(EncryptType type)
        {
            switch (type)
            {
                case EncryptType.SHA1:
                    throw new Exception("还未实现ＳＨＡ１加密算法");
                case EncryptType.DES:
                    throw new Exception("还未实现ＤＥＳ加密算法");
                case EncryptType.Rijndael:
                default:
                    _rijndael = new RijndaelEncrypt();
                    _encrypt = (IEncrypt)_rijndael;
                    break;
            }
        }

        #region IEncrypt 成员

        public string Encrypt(string password)
        {
            return _encrypt.Encrypt(password);
        }

        public string Decrypt(string password)
        {
            return _encrypt.Decrypt(password);
        }

        public byte[] Encrypt(ref byte[] passowrd)
        {
            return _encrypt.Encrypt(ref passowrd);
        }

        public byte[] Decrypt(ref byte[] password)
        {
            return _encrypt.Decrypt(ref password);
        }

        #endregion
    }

    public static class MD5Encrypt
    {
        #region MD5
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string GetMd5Hash(string input)
        {
            if (input == null || input.Equals("")) return "";

            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    public static class Encrypt
    {
        #region MD5
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string GetMd5Hash(string input)
        {
            if (input == null || input.Equals("")) return "";

            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        //public static void SetMemory()
        //{
        //    System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
        //    p.MaxWorkingSet = IntPtr.Add(IntPtr.Zero, 10 * 1024 * 1024);
        //    p.MinWorkingSet = IntPtr.Add(IntPtr.Zero, 5 * 1024 * 1024);
        //}

        #region Base64 加解密
        public static string EncodeBase64(string source)
        {
            try
            {
                byte[] bytes_1 = Encoding.Default.GetBytes(source);
                return Convert.ToBase64String(bytes_1).Replace("+", " ");
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string DecodeBase64(string result)
        {
            try
            {
                byte[] bytes_2 = Convert.FromBase64String(result.Replace(" ", "+"));
                return Encoding.Default.GetString(bytes_2);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

    }
}
