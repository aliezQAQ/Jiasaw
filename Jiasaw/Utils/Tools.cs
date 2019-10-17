using Jiasaw.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jiasaw.Utils
{
    public static class Tools
    {
        public static string ToShortMD5(string content)
        {
            string result = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            md5.Clear();
            md5.Dispose();
            StringBuilder builder = new StringBuilder(result);
            for (int i = 0; i < bt.Length; i++)
            {
                builder.Append(bt[i].ToString("X").PadLeft(2, '0'));
            }
            return builder.ToString().Substring(8, 16);
        }

    }
}
