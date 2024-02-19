using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;



namespace billservice.models.spiapi.BoardTake.models.db
{
    /// <summary>
    /// 条码获取主表
    /// </summary>
    public class SPIBOARDTAKE
    {
        /// <summary>
        /// id
        /// </summary>
        [Column(IsPrimary = true)]
        public long IDS
        {
            get; set;
        }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARKS
        {
            get; set;
        }

        /// <summary>
        /// 添加人
        /// </summary>
        public int? ADDUSERID
        {
            get; set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? ADDTIME
        {
            get; set;
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public int? UPDATEUSERID
        {
            get; set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UPDATETIME
        {
            get; set;
        }


        /// <summary>
        /// 删除人
        /// </summary>
        public int? DELETEUSERID
        {
            get; set;
        }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DELETETIME
        {
            get; set;
        }


        /// <summary>
        /// 是删除
        /// </summary>
        public int ISDELETE
        {
            get; set;
        }

        /// <summary>
        /// 客户端ip地址
        /// </summary>
        public string REMOTEIPADDRESS
        {
            get; set;
        }

        /// <summary>
        /// 程序版本号 
        /// </summary>
        public string APPVERSION
        {
            get; set;
        }




    }
}
