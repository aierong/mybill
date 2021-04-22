using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;

namespace billservice.interfaces
{
    public interface IUser
    {
        /// <summary>
        /// 是存在用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool IsExistUser ( string mobile );

        bool IsExistEmail ( string email );


        bool SaveUser ( users user ) ;


        users GetUser ( string mobile );
    }
}
