using System;
using System.Collections.Generic;
using System.Text;

namespace billservice.models.Dto
{
    public class UserDto
    {

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

        public DateTime? lastlogindate
        {
            get; set;
        }

        public int logintimes
        {
            get; set;
        }









    }
}
