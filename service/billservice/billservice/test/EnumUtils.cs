using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel;

namespace Utils
{
    public class EnumUtil
    {


        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription ( Enum value )
        {
            DescriptionAttribute[] customAttributes = ( DescriptionAttribute[] )value.GetType().GetField( value.ToString() ).GetCustomAttributes( typeof( DescriptionAttribute ) , false );
            if ( customAttributes.Length > 0 )
            {
                return customAttributes[0].Description;
            }
            return value.ToString();
        }



        /// <summary>
        /// 枚举转化为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int EnumConvertInt ( Enum value )
        {
            //枚举转化为数字
            int num = Convert.ToInt32( value.ToString( "D" ) );   //大写D,小写d都可以

            return num;
        }



        /// <summary>
        /// 判断字符串是否为枚举
        /// </summary>
        /// <param name="enumType">类型</param>
        /// <param name="str">字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool IsEnum ( Type enumType , string str , bool ignoreCase = true )
        {
            try
            {
                //Enum.Parse 方法的第2个参数,value 为空字符串或只包含空白或value 是一个名称，但不是为该枚举定义的已命名常数之一就发生异常
                //方法的第3个参数:true为忽略大小写,false为考虑大小写
                Enum.Parse( enumType , str , ignoreCase );

                return true;
            }
            catch ( ArgumentException )
            {
                return false;
            }
            finally
            {
            }
        }



        /// <summary>
        /// 判断数值是否为枚举
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static bool IsEnum ( Type enumType , int val )
        {
            if ( Enum.IsDefined( enumType , val ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 数字转换枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static T ConvertEnum<T> ( Type enumType , int val )
        {
            //调用本方法之前，请用IsEnum方法，判断一下，错误的值会报错的

            T _enum = ( T )System.Enum.Parse( enumType , val.ToString() );

            return _enum;
        }




        /// <summary>
        /// 字符串转换枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="str">The string.</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static T ConvertEnum<T> ( Type enumType , string str , bool ignoreCase = true )
        {
            //调用本方法之前，请用IsEnum方法，判断一下，错误的值会报错的

            T _enum = ( T )System.Enum.Parse( enumType , str , ignoreCase );

            return _enum;
        }






    }




}
