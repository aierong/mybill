using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.spiapi.BoardBind.Dto
{
    /// <summary>
    /// 条码绑定返回数据类
    /// </summary>
    public class BoardBindReturnDto
    {

        /// <summary>
        /// 条码
        /// </summary>
        public string BARCODE
        {
            get; set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string SERIALNO
        {
            get; set;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string MESSAGE
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// 成功标识 1=成功
        /// </summary>
        public int RESULT
        {
            get; set;
        }
    }
}


