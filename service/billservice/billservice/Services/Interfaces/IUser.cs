using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billservice.Services.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// 是存在用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool IsExistUser ( string mobile );
    }
}
