using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    /// <summary>
    /// 各种验证帮助类
    /// </summary>
    /// 作者:aierong
    /// 日期:2017/12/5
    /// 时间:17:01
    /// 电脑:YANFA1
    public class ValidatorHelper
    {
        /// <summary>
        /// 是有效邮件
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/8/17
        /// 时间:13:49
        /// 电脑:YANFA1
        static public bool IsEmail( string email )
        {
            return System.Text.RegularExpressions.Regex.IsMatch( email , @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" );
        }



        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/11/28
        /// 时间:16:17
        /// 电脑:YANFA1
        public static bool IsUrl( string source )
        {
            return System.Text.RegularExpressions.Regex.IsMatch( source , @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$" , System.Text.RegularExpressions.RegexOptions.IgnoreCase );
        }



        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/11/28
        /// 时间:16:21
        /// 电脑:YANFA1
        public static bool IsIP( string source )
        {
            return System.Text.RegularExpressions.Regex.IsMatch( source , @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$" , System.Text.RegularExpressions.RegexOptions.IgnoreCase );
        }



        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/11/28
        /// 时间:16:20
        /// 电脑:YANFA1
        public bool IsMobile( string input )
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex( @"^1[3|4|5|8]\d{9}$" );

            return regex.IsMatch( input );
        }



        /// <summary>
        /// 是奇数
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/3/8
        /// 时间:10:41
        /// 电脑:YANFA1
        public static bool IsOdd( int n )
        {
            while( true )
            {
                switch( n )
                {
                    case 1:
                        return true;
                    case 0:
                        return false;
                }
                n -= 2;
            }
        }



        /// <summary>
        /// 是不是整型数
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// 作者:chenghao
        /// 日期:2017/11/28
        /// 时间:16:27
        /// 电脑:YANFA1
        public static bool IsInteger( string source )
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex( @"^(-){0,1}\d+$" );
            if( regex.Match( source ).Success )
            {
                if( ( long.Parse( source ) > 0x7fffffffL ) || ( long.Parse( source ) < -2147483648L ) )
                {
                    return false;
                }
                return true;
            }
            return false;
        }

    }
}
