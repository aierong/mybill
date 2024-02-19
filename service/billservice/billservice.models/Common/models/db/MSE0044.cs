using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace spiapi.models.Common.models.db
{
    public class MSE0044
    {
        /// <summary>
        /// 物料id
        /// </summary>
        [Column(IsPrimary = true)]
        public int F001
        {
            get; set;
        }

        /// <summary>
        /// 条码
        /// </summary>
        [Column(IsPrimary = true)]
        public string F002
        {
            get; set;
        }

        /// <summary>
        /// 流程资料id
        /// </summary>
        public int F003
        {
            get; set;
        }

        /// <summary>
        /// 状态 G是良品，N是不良品，大写H还有写小h是暂缓
        /// </summary>
        public string F004
        {
            get; set;
        }



    }
}
