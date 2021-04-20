using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
问题简介： 返回DateTime,前端接收到的字符有时候为2018-01-01T12:01:01，有时候为2018-01-01T01:01:01.722+08:00，无法正常解析

*/

namespace billservice.Helpers
{
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read ( ref Utf8JsonReader reader , Type typeToConvert , JsonSerializerOptions options )
        {
            
            if ( reader.TokenType == JsonTokenType.String )
            {
                if ( DateTime.TryParse( reader.GetString() , out DateTime date ) )
                    return date;
            }

            return reader.GetDateTime();
        }

        public override void Write ( Utf8JsonWriter writer , DateTime value , JsonSerializerOptions options )
        {
            
            writer.WriteStringValue( value.ToString( "yyyy-MM-dd HH:mm:ss" ) );
        }
    }
}
