using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace billservice.Utils
{
    public class DataRowHelper
    {



 


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




        public static T SafeReadRow<T> ( DataRow row , string field )
        {
            // 
            return SafeReadRow( row , field , default( T ) );
        }




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
