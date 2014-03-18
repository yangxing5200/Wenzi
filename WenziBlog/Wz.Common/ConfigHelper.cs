using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Maticsoft.Common
{
	/// <summary>
	/// web.config操作类
    /// Copyright (C) Maticsoft
	/// </summary>
	public sealed class ConfigHelper
	{
		/// <summary>
		/// 得到AppSettings中的配置字符串信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetConfigString(string key)
		{
            string CacheKey = "AppSettings-" + key;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key]; 
                    if (objModel != null)
                    {                        
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
		}

		/// <summary>
		/// 得到AppSettings中的配置Bool信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetConfigBool(string key)
		{
			bool result = false;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = bool.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}
			return result;
		}
		/// <summary>
		/// 得到AppSettings中的配置Decimal信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static decimal GetConfigDecimal(string key)
		{
			decimal result = 0;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = decimal.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}
		/// <summary>
		/// 得到AppSettings中的配置int信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static int GetConfigInt(string key)
		{
			int result = 0;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = int.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
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

            public static void SetMemory()
            {
                System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
                p.MaxWorkingSet = IntPtr.Add(IntPtr.Zero, 10 * 1024 * 1024);
                p.MinWorkingSet = IntPtr.Add(IntPtr.Zero, 5 * 1024 * 1024);
            }

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
}
