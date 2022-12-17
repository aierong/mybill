using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;

namespace billservice.interfaces
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 是存在用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool IsExistUser ( string mobile );



        bool IsExistEmail ( string email );



        bool Save ( users user );

        Task<bool> SaveAsync ( users user );



        users GetUser ( string mobile );

        Task<users> GetUserAsync ( string mobile );



        bool UpdateAvatar ( string mobile , string avatar );

        Task<bool> UpdateAvatarAsync ( string mobile , string avatar );



        bool UpdatePassWord ( string mobile , string pwd );

        Task<bool> UpdatePassWordAsync ( string mobile , string pwd );



        bool UpdateLoginInfo ( string mobile , int logintimes );

        Task<bool> UpdateLoginInfoAsync ( string mobile , int logintimes );

    }
}
