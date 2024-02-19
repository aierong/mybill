using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace spiapi.models.BoardBind.models.db
{
    /// <summary>
    /// 
    /// </summary>
    public class SPIBOARDBINDS
    {
        /// <summary>
        /// id
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public long IDS
        {
            get; set;
        }

        /// <summary>
        /// 主表id
        /// </summary>
        public long PID
        {
            get; set;
        }

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
        /// 是获取过的条码  1：获取过 0：没有
        /// </summary>
        public int ISTAKE
        {
            get; set;
        }

        /// <summary>
        /// 程序检测结果状态 1=成功 0=失败
        /// </summary>
        public int ISCHECKOK
        {
            get; set;
        }

        /// <summary>
        /// 程序检测错误消息
        /// </summary>
        public string CHECKMSG
        {
            get; set;
        }
    }
}
