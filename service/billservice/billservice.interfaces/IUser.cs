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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsExistEmail ( string email );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Save ( users user );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> SaveAsync ( users user );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        users GetUser ( string mobile );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        Task<users> GetUserAsync ( string mobile );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        bool UpdateAvatar ( string mobile , string avatar );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        Task<bool> UpdateAvatarAsync ( string mobile , string avatar );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        bool UpdatePassWord ( string mobile , string pwd );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Task<bool> UpdatePassWordAsync ( string mobile , string pwd );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="logintimes"></param>
        /// <returns></returns>
        bool UpdateLoginInfo ( string mobile , int logintimes );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="logintimes"></param>
        /// <returns></returns>
        Task<bool> UpdateLoginInfoAsync ( string mobile , int logintimes );

    }
}
