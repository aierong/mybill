using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiapi.models.BoardBind.Dto
{
    /// <summary>
    /// 条码绑定接收类
    /// </summary>
    public class BoardBindDto
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
        /// 组标识(每次请求请保持唯一性)
        /// </summary>
        public string GROUPID
        {
            get; set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int COUNTS
        {
            get; set;
        } = 0;

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
        /// 明细表(条码信息列表)
        /// </summary>
        public List<BoardBindDlDto> BARCODELIST
        {
            get; set;
        }
    }
}


