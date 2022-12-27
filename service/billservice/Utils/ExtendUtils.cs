using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;


namespace Utils
{
    public static class ExtendUtils
    {
        public static bool IsDecimal ( this string value )
        {
            return Regex.IsMatch( value , @"^[+-]?\d*[.]?\d*$" );
        }



        public static bool GetBoolean ( this object value )
        {
            if ( value is bool )
                return ( bool )value;

            return GetBoolean( value.ToString() );
        }



        public static bool GetBoolean ( this string value )
        {
            if ( string.IsNullOrEmpty( value ) )
                return false;

                //|| value.Equals( "Y" , StringComparison.CurrentCultureIgnoreCase )
            else if ( value.Equals( "1" ) )
                return true;
            //|| value.Equals( "N" , StringComparison.CurrentCultureIgnoreCase )
            else if ( value.Equals( "0" ) )
                return false;
            else
                return Convert.ToBoolean( value );
        }



        public static decimal GetDecimal ( this object value )
        {
            if ( value == null )
                return 0;

            if ( value is decimal )
                return ( decimal )value;
            else if ( value is double )
                return ( decimal )( ( double )value );
            else if ( value is float )
                return ( decimal )( ( float )value );
            else if ( value is int )
                return ( decimal )( ( int )value );
            else if ( value is long )
                return ( decimal )( ( long )value );
            else
                return GetDecimal( value.ToString() );
        }



        public static decimal GetDecimal ( this string value )
        {
            if ( string.IsNullOrEmpty( value ) || !IsDecimal( value ) )
                return 0;
            else
                return Convert.ToDecimal( value );
        }



        public static int GetInteger ( this object value )
        {
            if ( value == null )
                return 0;

            if ( value is decimal )
                return ( int )( ( decimal )value );
            else if ( value is double )
                return ( int )( ( double )value );
            else if ( value is float )
                return ( int )( ( float )value );
            else if ( value is int )
                return ( int )value;
            else if ( value is long )
                return ( int )( ( long )value );
            else
                return GetInteger( value.ToString() );
        }

        public static int GetInteger ( this string value )
        {
            if ( string.IsNullOrEmpty( value ) || !IsDecimal( value ) )
                return 0;
            else
                return ( int )Convert.ToDecimal( value );
        }

        public static int GetInteger ( this string value , int defa )
        {
            if ( value.IsNullOrEmpty() )
                return defa;
            else
                return value.GetInteger();
        }

        public static string GetString ( this object value )
        {
            if ( value == null || value is DBNull )
                return string.Empty;
            else if ( value is string )
                return ( string )value;
            return value.ToString();
        }

        public static string GetString ( this object value , string defaultString )
        {
            if ( string.IsNullOrEmpty( value.GetString() ) )
            {
                return defaultString;
            }
            return value.GetString();
        }

        /// <summary>
        /// 等效IsNullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty ( this string value )
        {
            return string.IsNullOrEmpty( value );
        }





















        #region  转半角的函数 (DBC case)

        /// 转半角的函数 (DBC case)  
        /// 全角空格为 12288，半角空格为 32  
        /// 其他字符半角 (33-126) 与全角 (65281-65374) 的对应关系是：均相差 65248  
        public static string ToDBC ( this string value )
        {
            if ( value.IsNullOrEmpty() )
                return null;

            char[] c = value.ToCharArray();
            for ( int i = 0 ; i < c.Length ; i++ )
            {
                if ( c[i] == 12288 )
                {
                    c[i] = ( char )32;
                    continue;
                }
                if ( c[i] > 65280 && c[i] < 65375 )
                    c[i] = ( char )( c[i] - 65248 );
            }
            return new string( c );
        }
        #endregion




















    }

}
