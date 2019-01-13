using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Yachne.Common
{
    public class Utils
    {
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <param name="isEasy"></param>
        /// <returns></returns>
        public static string GetRandomString(int len = 16, bool isEasy = false)
        {
            StringBuilder SB = new StringBuilder();
            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (!isEasy)
            {
                str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";//复杂字符
            }

            byte[] b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            Random rd = new Random(BitConverter.ToInt32(b, 0));

            for (int i = 0; i < len; i++)
            {
                SB.Append(str.Substring(rd.Next(0, str.Length - 1), 1));
            }
            return SB.ToString();
        }
    }
}
