using System;
using System.Linq;
using System.Text;
using FreeSql.DataAnnotations;

namespace billservice.models
{
    ///<summary>
    /// 单据
    ///</summary>
    public partial class bills
    {



        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Column( IsPrimary = true , IsIdentity = true )]
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


        public int moneyyear
        {
            get; set;
        }


        public int moneymonth
        {
            get; set;
        }


        public int moneyday
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
