using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillReturnTopDto
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<BillReturnDto> list
        {
            get; set;
        }

        /// <summary>
        /// 记录总条数
        /// </summary>
        public int counts
        {
            get; set;
        }
    }
}
