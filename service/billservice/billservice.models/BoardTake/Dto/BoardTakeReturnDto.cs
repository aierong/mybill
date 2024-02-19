using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace spiapi.models.BoardTake.Dto
{
    /// <summary>
    /// 条码获取接收类明细表
    /// </summary>
    public class BoardTakeReturnDto
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
