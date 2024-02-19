using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.zw.spiapi.Config
{
    public class CommonParameters
    {
        /// <summary>
        /// cache时间 分钟
        /// </summary>
        public int CacheTimeFromMinutes
        {
            get; set;
        }



        /// <summary>
        /// 是否使用后台job
        /// </summary>
        public int IsJob
        {
            get; set;
        }




    }
}
