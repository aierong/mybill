using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServiceApp.dataclass
{
    /// <summary>
    /// 应用程序配置数据模型类
    /// </summary>
    public class appsetupdata
    {
        /// <summary>
        /// 是测试
        /// </summary>
        public bool IsTest
        {
            get; set;
        }



        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress
        {
            get; set;
        }



        /// <summary>
        /// 邮件地址(列表表达形式)
        /// </summary>
        public List<string> EmailAddressList
        {
            get
            {
                if ( !string.IsNullOrEmpty( EmailAddress ) )
                {
                    var arr = EmailAddress.Split( Convert.ToChar( ";" ) );

                    if ( arr != null )
                    {
                        if ( arr.Length > 0 )
                        {
                            return arr.ToList();
                        }
                    }
                }

                return new List<string>() { };
            }
        }



    }
}
