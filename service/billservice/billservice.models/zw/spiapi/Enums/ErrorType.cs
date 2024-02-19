using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.zw.spiapi.Enums
{
    /// <summary>
    /// 错误类型
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 非生产条码且状态不明
        /// </summary>
        F001,

        /// <summary>
        /// 机种错误
        /// </summary>
        F002,

        /// <summary>
        /// 非当前站位
        /// </summary>
        F003,

        /// <summary>
        /// 条码已暂缓
        /// </summary>
        F004,

        /// <summary>
        /// 不良状态
        /// </summary>
        F005,

        /// <summary>
        /// 清洗板超时
        /// </summary>
        F006,

        /// <summary>
        /// 其他原因
        /// </summary>
        F007


    }
}
