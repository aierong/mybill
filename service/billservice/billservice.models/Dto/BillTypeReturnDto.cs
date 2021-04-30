using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillTypeReturnDto
    {

        public int ids
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool isout
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public bool issystemtype
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string typename
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string avatar
        {
            get; set;
        }

         

    }
}
