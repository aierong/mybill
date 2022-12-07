using System;
using System.Linq;
using System.Text;
using FreeSql.DataAnnotations;

namespace billservice.models
{
    ///<summary>
    /// 角色
    ///</summary>
    public partial class roles
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
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Column( IsPrimary = true )]
        public string name
        {
            get; set;
        }

    }
}
