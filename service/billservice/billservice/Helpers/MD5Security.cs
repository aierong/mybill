using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace billservice.Helpers
{
    /// <summary>
    /// MD5
    /// </summary>
    public class MD5Security
    {

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string Encrypt ( string Source )
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes( Source );
            // This is one implementation of the abstract class MD5.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash( data );
            return Convert.ToBase64String( result );

        }



        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static byte[] Encrypt ( byte[] Source )
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash( Source );
            return result;
        }
    }
}
