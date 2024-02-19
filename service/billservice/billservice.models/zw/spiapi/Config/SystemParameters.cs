using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.zw.spiapi.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemParameters
    {
        /// <summary>
        /// 公用参数
        /// </summary>
        public CommonParameters Common
        {
            get; set;
        }

        /// <summary>
        /// spi相关参数
        /// </summary>
        public SpiParameters Spi
        {
            get; set;
        }
    }
}
