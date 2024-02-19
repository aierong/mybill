using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billservice.models.spiapi.Common.models
{
    public class ResultData<T> : ResultData where T : class
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T RESULT
        {
            get; set;
        }
    }
}
