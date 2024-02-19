﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiapi.models.Common.models
{
    public class ResultData
    {
        /// <summary>
        /// 最终结果
        /// </summary>
        public bool IsOk
        {
            get; set;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg
        {
            get; set;
        } = string.Empty;
    }
}
