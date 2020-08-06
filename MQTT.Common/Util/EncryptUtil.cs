using System;
using System.Collections.Generic;
using System.Text;

namespace MQTT.API.Common.Util
{
    /// <summary>
    /// 加密工具类
    /// </summary>
    public class EncryptUtil
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string Base64Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base64Decode(string value)
        {
            var bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
