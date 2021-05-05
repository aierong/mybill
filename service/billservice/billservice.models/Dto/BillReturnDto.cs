using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class BillReturnDto
    {
        public int ids
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string mobile
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int billtypeid
        {
            get; set;
        }

        public string typename
        {
            get; set;
        }

        public string avatar
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
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public decimal moneys
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string moneydate
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string memo
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sources
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime adddate
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? updatedate
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? deletedate
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:N
        /// Nullable:False
        /// </summary>           
        public string delmark
        {
            get; set;
        }



    }
}
