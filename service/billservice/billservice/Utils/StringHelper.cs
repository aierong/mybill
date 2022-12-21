using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace billservice.Utils
{
    public class StringHelper
    {
        /// <summary>
        /// 分解字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="FenGe">The fen ge.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/11/27
        /// 时间:16:10
        /// 电脑:YANFA1
        public static string[] StringSplit( string str , char FenGe )
        {
            return str.Split( FenGe );
        }



        /// <summary>
        /// 分解字符串
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/11/27
        /// 时间:16:11
        /// 电脑:YANFA1
        public static string[] StringSplit( string str )
        {
            return StringSplit( str , Convert.ToChar( "," ) );
        }


    }
}
