using System;
using System.Linq;
using System.Text;
using FreeSql.DataAnnotations;

namespace billservice.models
{
    ///<summary>
    ///
    ///</summary>
    public partial class users
    {


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Column( IsIdentity = true )]
        public int ids
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:user
        /// Nullable:False
        /// </summary>           
        public string rolename
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Column( IsPrimary = true )]
        public string mobile
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

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string password
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string name
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string email
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? lastlogindate
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int logintimes
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
