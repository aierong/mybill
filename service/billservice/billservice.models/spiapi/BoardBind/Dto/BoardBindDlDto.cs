using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.spiapi.BoardBind.Dto
{
    /// <summary>
    /// 条码绑定接收类(明细表)
    /// </summary>
    public class BoardBindDlDto
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
    }
}


