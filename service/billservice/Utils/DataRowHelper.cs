using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Utils
{
    public class DataRowHelper
    {

        /// <summary>
        /// 安全读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T SafeReadRow<T> ( DataRow row , string field , T defaultValue )
        {
            try
            {
                if ( row == null )
                {
                    return defaultValue;
                }

                if ( row.Table.Columns.Contains( field ) && !row.IsNull( field ) )
                {
                    Type typeFromHandle = typeof( T );
                    Type underlyingType = Nullable.GetUnderlyingType( typeFromHandle );

                    if ( underlyingType != null )
                    {
                        return ( T ) Convert.ChangeType( row[field] , underlyingType );
                    }

                    return ( T ) Convert.ChangeType( row[field] , typeFromHandle );
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }



        /// <summary>
        /// 安全读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T SafeReadRow<T> ( DataRow row , string field )
        {
            // 
            return SafeReadRow( row , field , default( T ) );
        }



        /// <summary>
        /// 安全设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SafeSetRowValue<T> ( DataRow row , string field , T value )
        {
            try
            {
                if ( row == null )
                {
                    return false;
                }

                if ( row.Table.Columns.Contains( field ) )
                {
                    row[field] = value;

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }




    }
}
