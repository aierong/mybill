using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace billservice.models.test
{
    /// <summary>
    /// 消息类
    /// </summary>
    public class MsgClass
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsOk
        {
            get; set;
        }


        /// <summary>
        /// 消息
        /// </summary>
        public string Msg
        {
            get; set;
        }
    }
}
