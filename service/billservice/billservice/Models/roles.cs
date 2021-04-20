using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace billservice.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable( "roles" )]
    public partial class roles
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn( IsIdentity = true )]
        public int ids
        {
            get; set;
        }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn( IsPrimaryKey = true )]
        public string name
        {
            get; set;
        }

    }
}
