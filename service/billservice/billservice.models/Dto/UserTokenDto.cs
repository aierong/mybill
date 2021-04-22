using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billservice.models.Dto
{
    public class UserTokenDto
    {
        public string token
        {
            get;set;
        }

        public string mobile
        {
            get; set;
        }

        public string rolename
        {
            get; set;
        }
    }
}
