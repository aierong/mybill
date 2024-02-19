using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace spiapi.models.BoardTake.models.db
{
    /// <summary>
    /// 条码获取明细表
    /// </summary>
    public class SPIBOARDTAKES
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
        /// SPIBoardBinds表的id
        /// </summary>
        public long BINDSID
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

    }
}
