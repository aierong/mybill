using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.zw.spiapi.BoardTake.Dto
{
    /// <summary>
    /// 条码获取接收类
    /// </summary>
    public class BoardTakeDto
    {
        /// <summary>
        /// 过账的站位编号
        /// </summary>
        public string STATION
        {
            get; set;
        }

        /// <summary>
        /// 过账线体
        /// </summary>
        public string LINENO
        {
            get; set;
        }

        /// <summary>
        /// 机台编号
        /// </summary>
        public string MACHINE
        {
            get; set;
        }

        /// <summary>
        /// 扫描时间
        /// </summary>
        public string STARTTIME
        {
            get; set;
        }

        /// <summary>
        /// 一些附加数据
        /// </summary>
        public string TESTDETAIL
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
    }
}
